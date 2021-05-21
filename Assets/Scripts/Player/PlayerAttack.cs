using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
	public static PlayerAttack instance;

	public InventoryItem.ItemType currentWeaponItemType;
	public InventoryItem.ItemType lastWeaponItemType;
	public InventoryItem.ItemType parsedWeaponItemType;
	public GameObject weaponHolderBone;
	public LayerMask attackableLayerMask;
	public LayerMask shootableLayerMask;
	public NavMeshSurface navMesh;

	private Animator animator;
	private Camera playerCam;

	public float coolDownTime;
	public float bulletSpeed;
	public float coolDownCounter;
	private float fishingCounter;
	private float eatingCounter;

	private int secondsAfterCastedRod;
	private int fishBaitedTime;
	private int fishLeftTime;

	public bool coolingDown = false;
	private bool eating = false;
	private bool ate = false;

	private Vector3 positionWhenCastedRod;

	private enum FISHING_STATE
	{
		IDLE,
		CASTED,
		BOBBING
	}

	private FISHING_STATE fishingState = FISHING_STATE.IDLE;

	#region Weapon Objects
	public GameObject handObject;
	public GameObject workbenchObject;
	public GameObject furnaceObject;
	public GameObject cookerObject;
	public GameObject chestObject;
	public GameObject fishObject;
	public GameObject meatObject;
	public GameObject carrotObject;
	public GameObject fishingrodObject;
	public GameObject axeObject;
	public GameObject swordObject;
	public GameObject pickaxeObject;
	public GameObject houseObject;
	public GameObject logObject;
	public GameObject stoneObject;
	public GameObject cobblestoneObject;
	public GameObject boneObject;
	public GameObject skullObject;
	public GameObject portalObject;
	#endregion 

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
		playerCam = GetComponentInChildren<Camera>();
		coolDownCounter = coolDownTime;
		HoldWeapon(InventoryItem.ItemType.Hand);
	}

	private void Update()
	{
		lastWeaponItemType = currentWeaponItemType;
		currentWeaponItemType = parsedWeaponItemType;
		if (currentWeaponItemType != lastWeaponItemType)
		{
			//Weapon Changed
			ResetEating();
			ResetFishingRod(false);
			coolingDown = false;
			HoldWeapon(currentWeaponItemType);
		}

		if (fishingState == FISHING_STATE.CASTED || fishingState == FISHING_STATE.BOBBING)
		{
			fishingCounter += Time.deltaTime;
			secondsAfterCastedRod = (int)(fishingCounter % 60);
		}

		if (eating)
		{
			eatingCounter += Time.deltaTime;
			HUD.instance.UpdateLoadingCircle(eatingCounter);
		}
		else
		{
			eatingCounter = 0f;
			HUD.instance.UpdateLoadingCircle(0);
		}

		if (coolingDown)
		{
			coolDownCounter -= Time.deltaTime;
			if (coolDownCounter <= 0)
			{
				coolDownCounter = coolDownTime;
				coolingDown = false;
			}
		}

		if (fishingState == FISHING_STATE.CASTED || fishingState == FISHING_STATE.BOBBING)
		{
			if (transform.position != positionWhenCastedRod)
			{
				ResetFishingRod(true);
			}

			if (secondsAfterCastedRod >= fishBaitedTime)
			{
				animator.SetBool("FISHBOBBING", true);
				fishingState = FISHING_STATE.BOBBING;
				if (!AudioManager.instance.IsPlaying("BUBBLE")) AudioManager.instance.PlaySound("BUBBLE"); 
			}

			if (secondsAfterCastedRod >= fishLeftTime)
			{
				ResetFishingRod(true);
			}
		}

		if (eatingCounter >= 2.5f)
		{
			//ATE SOMETHING
			Inventory.instance.RemoveItem(currentWeaponItemType, 1);
			ResetEating();
			ate = true;
			if (currentWeaponItemType == InventoryItem.ItemType.Carrot) PlayerStats.instance.AddHunger(2);
			if (currentWeaponItemType == InventoryItem.ItemType.Meat) PlayerStats.instance.AddHunger(3);
		}

		if (eating)
		{
			animator.SetBool("EATING", true);
			if (!AudioManager.instance.IsPlaying("EATING")) AudioManager.instance.PlaySound("EATING");
		}
		else
		{
			animator.SetBool("EATING", false);
			
			AudioManager.instance.StopSound("EATING");
		}

		if (Input.GetMouseButtonUp(0))
		{
			ResetEating();
			ate = false;
		}

		if (!PlayerMovement.inventoryActive && !PlayerInteraction.interactingWithWorkstation && !StateHandler.instance.gamePaused)
		{
			if (Input.GetMouseButton(0) && IsHoldingFood() && !PlayerMovement.instance.sprinting && !ate) eating = true;
			else eating = false;

			if (Input.GetMouseButton(0) && !PlayerMovement.instance.sprinting && !coolingDown)
			{
				if (!PlayerMovement.instance.mountedToTurret)
                {
					switch (currentWeaponItemType)
					{
						case InventoryItem.ItemType.Axe:
						case InventoryItem.ItemType.Pickaxe:
						case InventoryItem.ItemType.Sword:
							//ATTACK
							bool hitSomething = false;
							Ray attackRay = playerCam.ScreenPointToRay(Input.mousePosition);
							RaycastHit attackHit;
							if (Physics.Raycast(attackRay, out attackHit, 100, attackableLayerMask))
							{
								Attackable attackableComponent = attackHit.collider.GetComponent<Attackable>();
								if (attackableComponent != null)
								{
									hitSomething = attackableComponent.AttackMe();
								}
							}
							animator.SetTrigger("ATTACK");
							if (!hitSomething) AudioManager.instance.PlaySound("SWING_WEAPON");
							coolingDown = true;
							break;
						case InventoryItem.ItemType.FishingRod:
							if (fishingState == FISHING_STATE.IDLE)
							{
								Ray fishingRay = playerCam.ScreenPointToRay(Input.mousePosition);
								RaycastHit fishingHit;

								if (Physics.Raycast(fishingRay, out fishingHit, 100))
								{
									if (fishingHit.collider.CompareTag("Water"))
									{
										positionWhenCastedRod = transform.position;
										animator.SetBool("CASTFISHINGROD", true);
										AudioManager.instance.PlaySound("SPLASH");
										fishingState = FISHING_STATE.CASTED;
										fishBaitedTime = 10;
										fishLeftTime = 15;
									}

								}
							}
							else if (fishingState == FISHING_STATE.CASTED) ResetFishingRod(true);
							else if (fishingState == FISHING_STATE.BOBBING)
							{
								//CAUGHT FISH
								Inventory.instance.AddItem(InventoryItem.ItemType.Fish, 1, true);
								ResetFishingRod(true);
							}
							coolingDown = true;
							break;
					}
				}
				else
                {
					Shootable shootableComponent = null;
					Ray shootRay = playerCam.ScreenPointToRay(Input.mousePosition);
					RaycastHit shootHit;
					if (Physics.Raycast(shootRay, out shootHit, 100f, shootableLayerMask))
					{
						ShootablePart part = shootHit.collider.GetComponent<ShootablePart>();
						if (part) shootableComponent = part.shootableBase;
					}

					if (shootableComponent) shootableComponent.BulletHit(shootHit.point);

					TurretItem.instance.muzzleFlash.Play();
					AudioManager.instance.PlaySound("SHOOT");
					coolingDown = true;
				}

			}

			if (Input.GetMouseButtonDown(1) && GameHandler.instance.currentWorld == GameHandler.WORLD.OVERWORLD)
			{

				//Place placeable objects
				if (PlaceableItemPrefabs.instance.GetObject(currentWeaponItemType))
				{
					//Object is Placeable
					//Place Object down
					Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, 100))
					{
						if (hit.collider.CompareTag("Ground"))
						{
							AudioManager.instance.PlaySound("PLACE_ITEM");
							Vector3 hitPoint = hit.point;
							GameObject placeableObject = PlaceableItemPrefabs.instance.GetObject(currentWeaponItemType);

							Vector3 spawnPosition;
							Quaternion spawnRotation;

							spawnPosition = new Vector3(hitPoint.x, placeableObject.transform.position.y, hitPoint.z);

							spawnRotation = gameObject.transform.rotation;
							float xRot = spawnRotation.eulerAngles.x;
							float yRot = spawnRotation.eulerAngles.y + placeableObject.transform.rotation.eulerAngles.y + 180;
							float zRot = spawnRotation.eulerAngles.z;
							spawnRotation = Quaternion.Euler(xRot, yRot, zRot);

							GameHandler.instance.PlaceOverworldObject(Instantiate(placeableObject, spawnPosition, spawnRotation));

							Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
							InventoryUI.instance.RefreshInventoryItems();

							UpdateNavMesh();

						}
					}
				}

			}
		}
		
	}

	public void ResetFishingRod(bool playSound)
	{
		if (playSound) AudioManager.instance.PlaySound("SPLASH");
		AudioManager.instance.StopSound("BUBBLE");
		fishingState = FISHING_STATE.IDLE;
		animator.SetBool("CASTFISHINGROD", false);
		animator.SetBool("FISHBOBBING", false);

		fishingCounter = 0f;
		fishBaitedTime = 0;
		fishLeftTime = 0;
		secondsAfterCastedRod = 0;
	}

	public void ResetEating()
	{
		eating = false;
		eatingCounter = 0;
		HUD.instance.UpdateLoadingCircle(0);
		animator.SetBool("EATING", false);
	}

	private void HoldWeapon(InventoryItem.ItemType weaponType)
	{
		Transform weaponHolderBoneTransform = weaponHolderBone.transform;
		foreach (Transform weapon in weaponHolderBoneTransform)
		{
			weapon.gameObject.SetActive(false);
		}
		GetObject(weaponType).SetActive(true);

	}

	private bool IsHoldingFood()
	{
		bool holdingFood = false;

		if (currentWeaponItemType == InventoryItem.ItemType.Carrot || currentWeaponItemType == InventoryItem.ItemType.Meat)
		{
			holdingFood = true;
		}

		return holdingFood;
	}

	private GameObject GetObject(InventoryItem.ItemType itemType)
	{
		GameObject returnedObject = null;
		switch (itemType)
		{
			default: break;
			case InventoryItem.ItemType.Hand: 
				returnedObject = handObject;
				break;
			case InventoryItem.ItemType.Workbench:
				returnedObject = workbenchObject;
				break;
			case InventoryItem.ItemType.Furnace:
				returnedObject = furnaceObject;
				break;
			case InventoryItem.ItemType.Cooker:
				returnedObject = cookerObject;
				break;
			case InventoryItem.ItemType.Chest:
				returnedObject = chestObject;
				break;
			case InventoryItem.ItemType.Fish:
				returnedObject = fishObject;
				break;
			case InventoryItem.ItemType.Meat:
				returnedObject = meatObject;
				break;
			case InventoryItem.ItemType.Carrot:
				returnedObject = carrotObject;
				break;
			case InventoryItem.ItemType.FishingRod:
				returnedObject = fishingrodObject;
				break;
			case InventoryItem.ItemType.Axe:
				returnedObject = axeObject;
				break;
			case InventoryItem.ItemType.Sword:
				returnedObject = swordObject;
				break;
			case InventoryItem.ItemType.Pickaxe:
				returnedObject = pickaxeObject;
				break;
			case InventoryItem.ItemType.House:
				returnedObject = houseObject;
				break;
			case InventoryItem.ItemType.Logs:
				returnedObject = logObject;
				break;
			case InventoryItem.ItemType.Stone:
				returnedObject = stoneObject;
				break;
			case InventoryItem.ItemType.Cobblestone:
				returnedObject = cobblestoneObject;
				break;
			case InventoryItem.ItemType.Bone:
				returnedObject = boneObject;
				break;
			case InventoryItem.ItemType.Skull:
				returnedObject = skullObject;
				break;
			case InventoryItem.ItemType.Portal:
				returnedObject = portalObject;
				break;
		}
		return returnedObject;
	}

	public void UpdateNavMesh()
    {
		GameObject navMeshObject = GameObject.FindGameObjectWithTag("NavMesh");
		if (navMeshObject != null) navMesh = navMeshObject.GetComponent<NavMeshSurface>();
		if (navMesh != null) navMesh.BuildNavMesh();
	}

}

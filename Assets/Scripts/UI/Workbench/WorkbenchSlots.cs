using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchSlots : MonoBehaviour
{
	public static WorkbenchSlots instance;

	private WorkbenchSlot[] craftableSlots;

	public GameObject workbenchUI;
	public WorkbenchSlot currentSelectedSlot;
	public IngredientSlot logs;
	public IngredientSlot cobblestone;
	public IngredientSlot stone;
	public IngredientSlot bone;
	public IngredientSlot skull;


	private void Awake()
	{
		instance = this;
		workbenchUI.SetActive(false);
	}

	private void Start()
	{
		craftableSlots = GetComponentsInChildren<WorkbenchSlot>();
	}

	public void UnSelectAllSlots()
	{
		for (int i = 0; i < craftableSlots.Length; i++)
		{
			WorkbenchSlot slot = craftableSlots[i];
			slot.ToggleSelect(false);
		}
	}

	public void RefreshIngredients()
	{
		Dictionary<InventoryItem.ItemType, int> ingredients = GetIngredients(currentSelectedSlot.itemType);
		logs.Initialize(ingredients[InventoryItem.ItemType.Logs]);
		cobblestone.Initialize(ingredients[InventoryItem.ItemType.Cobblestone]);
		stone.Initialize(ingredients[InventoryItem.ItemType.Stone]);
		bone.Initialize(ingredients[InventoryItem.ItemType.Bone]);
		skull.Initialize(ingredients[InventoryItem.ItemType.Skull]);
	}

	public void Craft()
	{
		if (currentSelectedSlot != null)
		{
			Dictionary<InventoryItem.ItemType, int> ingredients = GetIngredients(currentSelectedSlot.itemType);
			int logs = GetAmountOfItem(InventoryItem.ItemType.Logs);
			int cobblestone = GetAmountOfItem(InventoryItem.ItemType.Cobblestone);
			int stone = GetAmountOfItem(InventoryItem.ItemType.Stone);
			int bones = GetAmountOfItem(InventoryItem.ItemType.Bone);
			int skull = GetAmountOfItem(InventoryItem.ItemType.Skull);

			int logsNeeded = ingredients[InventoryItem.ItemType.Logs];
			int cobblestonesNeeded = ingredients[InventoryItem.ItemType.Cobblestone];
			int stonesNeeded = ingredients[InventoryItem.ItemType.Stone];
			int bonesNeeded = ingredients[InventoryItem.ItemType.Bone];
			int skullsNeeded = ingredients[InventoryItem.ItemType.Skull];

			bool passLogs = false;
			bool passCobblestone = false;
			bool passStones = false;
			bool passBones = false;
			bool passSkulls = false;

			if (logs >= logsNeeded) passLogs = true;
			if (cobblestone >= cobblestonesNeeded) passCobblestone = true;
			if (stone >= stonesNeeded) passStones = true;
			if (bones >= bonesNeeded) passBones = true;
			if (skull >= skullsNeeded) passSkulls = true;

			if (passLogs && passCobblestone && passStones && passBones && passSkulls)
			{
				//CAN CRAFT
				AudioManager.instance.PlaySound("CLICK");
				Inventory.instance.RemoveItem(InventoryItem.ItemType.Logs, logsNeeded);
				Inventory.instance.RemoveItem(InventoryItem.ItemType.Cobblestone, cobblestonesNeeded);
				Inventory.instance.RemoveItem(InventoryItem.ItemType.Stone, stonesNeeded);
				Inventory.instance.RemoveItem(InventoryItem.ItemType.Bone, bonesNeeded);
				Inventory.instance.RemoveItem(InventoryItem.ItemType.Skull, skullsNeeded);
				Inventory.instance.AddItem(currentSelectedSlot.itemType, 1, false);

			}
		}
	}

	public Dictionary<InventoryItem.ItemType, int> GetIngredients(InventoryItem.ItemType itemToCraft)
	{
		Dictionary<InventoryItem.ItemType, int> returnedDictionary = new Dictionary<InventoryItem.ItemType, int>();
		returnedDictionary.Add(InventoryItem.ItemType.Logs, 0);
		returnedDictionary.Add(InventoryItem.ItemType.Cobblestone, 0);
		returnedDictionary.Add(InventoryItem.ItemType.Stone, 0);
		returnedDictionary.Add(InventoryItem.ItemType.Bone, 0);
		returnedDictionary.Add(InventoryItem.ItemType.Skull, 0);
		switch (itemToCraft)
		{
			default:
			case InventoryItem.ItemType.Hand:
			case InventoryItem.ItemType.Fish:
			case InventoryItem.ItemType.Meat:
			case InventoryItem.ItemType.Carrot:
			case InventoryItem.ItemType.Axe:
			case InventoryItem.ItemType.Pickaxe:
			case InventoryItem.ItemType.Logs:
			case InventoryItem.ItemType.Stone:
			case InventoryItem.ItemType.Bone:
				returnedDictionary = null;
				break;
			case InventoryItem.ItemType.Sword:
				returnedDictionary[InventoryItem.ItemType.Logs] = 2;
				returnedDictionary[InventoryItem.ItemType.Cobblestone] = 4;
				break;
			case InventoryItem.ItemType.FishingRod:
				returnedDictionary[InventoryItem.ItemType.Logs] = 2;
				returnedDictionary[InventoryItem.ItemType.Cobblestone] = 5;
				break;
			case InventoryItem.ItemType.House:
				returnedDictionary[InventoryItem.ItemType.Logs] = 10;
				returnedDictionary[InventoryItem.ItemType.Cobblestone] = 20;
				break;
			case InventoryItem.ItemType.Cobblestone:
				returnedDictionary[InventoryItem.ItemType.Stone] = 4;
				break;
			case InventoryItem.ItemType.Workbench:
				returnedDictionary[InventoryItem.ItemType.Logs] = 6;
				break;
			case InventoryItem.ItemType.Furnace:
				returnedDictionary[InventoryItem.ItemType.Logs] = 2;
				returnedDictionary[InventoryItem.ItemType.Stone] = 6;
				break;
			case InventoryItem.ItemType.Cooker:
				returnedDictionary[InventoryItem.ItemType.Logs] = 2;
				returnedDictionary[InventoryItem.ItemType.Cobblestone] = 3;
				break;
			case InventoryItem.ItemType.Chest:
				returnedDictionary[InventoryItem.ItemType.Logs] = 8;
				break;
			case InventoryItem.ItemType.Skull:
				returnedDictionary[InventoryItem.ItemType.Bone] = 10;
				break;
			case InventoryItem.ItemType.Portal:
				returnedDictionary[InventoryItem.ItemType.Skull] = 10;
				returnedDictionary[InventoryItem.ItemType.Cobblestone] = 20;
				break;
		}

		return returnedDictionary;
	}

	private int GetAmountOfItem(InventoryItem.ItemType itemType)
	{
		int amount = 0;
		foreach (InventoryItem item in Inventory.instance.items)
		{
			if (item.itemType == itemType)
			{
				amount = item.amount;
				break;
			}
		}

		return amount;
	}

}

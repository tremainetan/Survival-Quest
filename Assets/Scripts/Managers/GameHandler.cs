using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameHandler : MonoBehaviour
{

	public static GameHandler instance;

	public enum WORLD
	{
		OVERWORLD,
		UNDERWORLD
	}

	public WORLD currentWorld;
	public GameObject defaultItems;
	public GameObject welcomeSign;
	public int health;
	public int hunger;

	public List<PlaceableItemData.ChestData> chestObjects = new List<PlaceableItemData.ChestData>();
	public List<PlaceableItemData.CookerData> cookerObjects = new List<PlaceableItemData.CookerData>();
	public List<PlaceableItemData.FurnaceData> furnaceObjects = new List<PlaceableItemData.FurnaceData>();
	public List<PlaceableItemData.HouseData> houseObjects = new List<PlaceableItemData.HouseData>();
	public List<PlaceableItemData.PortalData> portalObjects = new List<PlaceableItemData.PortalData>();
	public List<PlaceableItemData.WorkbenchData> workbenchObjects = new List<PlaceableItemData.WorkbenchData>();

	public List<GameObject> overworldPlacedObjects = new List<GameObject>();
	public List<InventoryItem> inventory = new List<InventoryItem>();

	private void Awake()
	{
		if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}

    private void Start()
    {
		
		DownloadData();

		SerializableData data = SerializeScript.LoadData();
		if (data.statsInitialised) PrivateLoadOverworld();
		else
		{
			Instantiate(defaultItems);
			Instantiate(welcomeSign);
		}
	}

    private void Update()
	{
		//Remove All null values in Placed Objects List
		overworldPlacedObjects.RemoveAll(placedObject => placedObject == null);
	}

    private void OnApplicationQuit()
    {
		UploadData();
	}

    public void LoadOverworld()
    {
		if (currentWorld != WORLD.OVERWORLD) PrivateLoadOverworld();
	}

	private void PrivateLoadOverworld()
    {
		Inventory.instance.ParseInventory(inventory);
		PlayerStats.instance.ParseStats(health, hunger);

		foreach (PlaceableItemData.ChestData data in chestObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.Chest)));
		foreach (PlaceableItemData.CookerData data in cookerObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.Cooker)));
		foreach (PlaceableItemData.FurnaceData data in furnaceObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.Furnace)));
		foreach (PlaceableItemData.HouseData data in houseObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.House)));
		foreach (PlaceableItemData.PortalData data in portalObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.Portal)));
		foreach (PlaceableItemData.WorkbenchData data in workbenchObjects) data.PlaceObject(Instantiate(PlaceableItemPrefabs.instance.GetObject(InventoryItem.ItemType.Workbench)));

	}

	public void LoadUnderworld()
    {
		if (currentWorld != WORLD.UNDERWORLD) PrivateLoadUnderworld();
	}

	private void PrivateLoadUnderworld()
    {
		Inventory.instance.ParseInventory(inventory);
		PlayerStats.instance.ParseStats(health, hunger);
    }
	
	public void SaveUnderworldState()
    {
		inventory = Inventory.instance.items;
		health = PlayerStats.instance.health;
		hunger = PlayerStats.instance.hunger;
    }

	public void SaveOverworldState()
	{
		health = PlayerStats.instance.health;
		hunger = PlayerStats.instance.hunger;
		inventory = Inventory.instance.items;

		chestObjects.Clear();
		cookerObjects.Clear();
		furnaceObjects.Clear();
		houseObjects.Clear();
		portalObjects.Clear();
		workbenchObjects.Clear();

		foreach (GameObject obj in overworldPlacedObjects)
        {
			Placeable placeableScript = obj.GetComponentInChildren<Placeable>();
			if (placeableScript == null) return;

			InventoryItem.ItemType objectType = placeableScript.droppedItemWhenFailed;
			switch (objectType)
			{
				case InventoryItem.ItemType.Chest:
					chestObjects.Add(new PlaceableItemData.ChestData(obj));
					break;
				case InventoryItem.ItemType.Cooker:
					cookerObjects.Add(new PlaceableItemData.CookerData(obj));
					break;
				case InventoryItem.ItemType.Furnace:
					furnaceObjects.Add(new PlaceableItemData.FurnaceData(obj));
					break;
				case InventoryItem.ItemType.House:
					houseObjects.Add(new PlaceableItemData.HouseData(obj));
					break;
				case InventoryItem.ItemType.Portal:
					portalObjects.Add(new PlaceableItemData.PortalData(obj));
					break;
				case InventoryItem.ItemType.Workbench:
					workbenchObjects.Add(new PlaceableItemData.WorkbenchData(obj));
					break;

			}
		}

		overworldPlacedObjects.Clear();

	}

	public void PlaceOverworldObject(GameObject obj)
	{
		overworldPlacedObjects.Add(obj);
	}

	public void DownloadData()
	{
		SerializableData data = SerializeScript.LoadData();
		health = data.health;
		hunger = data.hunger;

		inventory = data.inventory;

		chestObjects = data.chestObjects;
		cookerObjects = data.cookerObjects;
		furnaceObjects = data.furnaceObjects;
		houseObjects = data.houseObjects;
		portalObjects = data.portalObjects;
		workbenchObjects = data.workbenchObjects;
	}

	public void UploadData()
    {
		if (currentWorld == WORLD.OVERWORLD) SaveOverworldState();
		else SaveUnderworldState();

		SerializableData data = SerializeScript.LoadData();

		data.health = health;
		data.hunger = hunger;

		data.inventory = inventory;

		data.chestObjects = chestObjects;
		data.cookerObjects = cookerObjects;
		data.furnaceObjects = furnaceObjects;
		data.houseObjects = houseObjects;
		data.portalObjects = portalObjects;
		data.workbenchObjects = workbenchObjects;

		data.statsInitialised = true;

		SerializeScript.SaveData(data);

	}

	public void ResetData()
    {
		SerializableData oldData = SerializeScript.LoadData();
		SerializableData newData = new SerializableData(oldData.highestSkeletonKills, oldData.highestBossKills);
		SerializeScript.SaveData(newData);
    }

	public void UninitialiseStats()
    {
		SerializableData data = SerializeScript.LoadData();
		data.statsInitialised = false;
		SerializeScript.SaveData(data);
    }

}

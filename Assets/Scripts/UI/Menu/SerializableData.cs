using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableData
{

    public int highestSkeletonKills;
    public int highestBossKills;

    public bool statsInitialised = false;

    public int health;
    public int hunger;

    public List<PlaceableItemData.ChestData> chestObjects = new List<PlaceableItemData.ChestData>();
    public List<PlaceableItemData.CookerData> cookerObjects = new List<PlaceableItemData.CookerData>();
    public List<PlaceableItemData.FurnaceData> furnaceObjects = new List<PlaceableItemData.FurnaceData>();
    public List<PlaceableItemData.HouseData> houseObjects = new List<PlaceableItemData.HouseData>();
    public List<PlaceableItemData.PortalData> portalObjects = new List<PlaceableItemData.PortalData>();
    public List<PlaceableItemData.WorkbenchData> workbenchObjects = new List<PlaceableItemData.WorkbenchData>();

    public List<InventoryItem> inventory = new List<InventoryItem>();

    public SerializableData(int highestSkeletonKills, int highestBossKills)
    {
        this.highestSkeletonKills = highestSkeletonKills;
        this.highestBossKills = highestBossKills;
    }

}

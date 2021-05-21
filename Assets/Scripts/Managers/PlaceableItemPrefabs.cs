using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableItemPrefabs : MonoBehaviour
{

    public static PlaceableItemPrefabs instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
    }

    public GameObject workbenchObject;
    public GameObject furnaceObject;
    public GameObject cookerObject;
    public GameObject chestObject;
    public GameObject houseObject;
    public GameObject portalObject;

    public GameObject GetObject(InventoryItem.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case InventoryItem.ItemType.Pickaxe:
            case InventoryItem.ItemType.Sword:
            case InventoryItem.ItemType.Hand:
            case InventoryItem.ItemType.Axe:
            case InventoryItem.ItemType.FishingRod:
            case InventoryItem.ItemType.Logs:
            case InventoryItem.ItemType.Stone:
            case InventoryItem.ItemType.Cobblestone:
            case InventoryItem.ItemType.Fish:
            case InventoryItem.ItemType.Meat:
            case InventoryItem.ItemType.Carrot:
            case InventoryItem.ItemType.Bone:
            case InventoryItem.ItemType.Skull:
                return null;
            case InventoryItem.ItemType.Workbench:
                return workbenchObject;
            case InventoryItem.ItemType.Furnace:
                return furnaceObject;
            case InventoryItem.ItemType.Cooker:
                return cookerObject;
            case InventoryItem.ItemType.Chest:
                return chestObject;
            case InventoryItem.ItemType.House:
                return houseObject;
            case InventoryItem.ItemType.Portal:
                return portalObject;
        }
    }


}
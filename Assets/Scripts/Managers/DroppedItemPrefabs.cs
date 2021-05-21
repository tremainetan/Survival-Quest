using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemPrefabs : MonoBehaviour
{

    public static DroppedItemPrefabs instance;

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

    public GameObject GetObject(InventoryItem.ItemType itemType)
    {
        GameObject returnedObject = null;
        switch (itemType)
        {
            default: break;
            case InventoryItem.ItemType.Hand: break;
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


}
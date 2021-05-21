using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public enum ItemType
    {
        Hand,
        Workbench,
        Furnace,
        Cooker,
        Chest,
        Fish,
        Meat,
        Carrot,
        FishingRod,
        Axe,
        Sword,
        Pickaxe,
        House,
        Logs,
        Stone,
        Cobblestone,
        Bone,
        Skull,
        Portal
    }

    public ItemType itemType = ItemType.Hand;
    public int amount;

    public InventoryItem(ItemType itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
    }

    public Sprite GetSprite()
    {
        Sprite returnedSprite = null;
        switch (itemType)
        {
            default: break;
            case ItemType.Hand: break;
            case ItemType.Workbench: 
                returnedSprite = ItemSprites.instance.workbenchSprite;
                break;
            case ItemType.Furnace: 
                returnedSprite = ItemSprites.instance.furnaceSprite;
                break;
            case ItemType.Cooker: 
                returnedSprite = ItemSprites.instance.cookerSprite;
                break;
            case ItemType.Chest: 
                returnedSprite = ItemSprites.instance.chestSprite;
                break;
            case ItemType.Fish: 
                returnedSprite = ItemSprites.instance.fishSprite;
                break;
            case ItemType.Meat: 
                returnedSprite = ItemSprites.instance.meatSprite;
                break;
            case ItemType.Carrot: 
                returnedSprite = ItemSprites.instance.carrotSprite;
                break;
            case ItemType.FishingRod: 
                returnedSprite = ItemSprites.instance.fishingrodSprite;
                break;
            case ItemType.Axe: 
                returnedSprite = ItemSprites.instance.axeSprite;
                break;
            case ItemType.Sword: 
                returnedSprite = ItemSprites.instance.swordSprite;
                break;
            case ItemType.Pickaxe: 
                returnedSprite = ItemSprites.instance.pickaxeSprite;
                break;
            case ItemType.House: 
                returnedSprite = ItemSprites.instance.houseSprite;
                break;
            case ItemType.Logs: 
                returnedSprite = ItemSprites.instance.logSprite;
                break;
            case ItemType.Stone:
                returnedSprite = ItemSprites.instance.stoneSprite;
                break;
            case ItemType.Cobblestone: 
                returnedSprite = ItemSprites.instance.cobblestoneSprite;
                break;
            case ItemType.Bone:
                returnedSprite = ItemSprites.instance.boneSprite;
                break;
            case ItemType.Skull:
                returnedSprite = ItemSprites.instance.skullSprite;
                break;
            case ItemType.Portal:
                returnedSprite = ItemSprites.instance.portalSprite;
                break;
        }
        return returnedSprite;
    }

    public string GetName()
    {
        string returnedName = "";
        switch (itemType)
        {
            default: break;
            case ItemType.Hand:
                returnedName = "Hand";
                break;
            case ItemType.Workbench:
                returnedName = "Workbench";
                break;
            case ItemType.Furnace:
                returnedName = "Furnace";
                break;
            case ItemType.Cooker:
                returnedName = "Cooker";
                break;
            case ItemType.Chest:
                returnedName = "Chest";
                break;
            case ItemType.Fish:
                returnedName = "Fish";
                break;
            case ItemType.Meat:
                returnedName = "Meat";
                break;
            case ItemType.Carrot:
                returnedName = "Carrot";
                break;
            case ItemType.FishingRod:
                returnedName = "Fishing Rod";
                break;
            case ItemType.Axe:
                returnedName = "Axe";
                break;
            case ItemType.Sword:
                returnedName = "Sword";
                break;
            case ItemType.Pickaxe:
                returnedName = "Pickaxe";
                break;
            case ItemType.House:
                returnedName = "House";
                break;
            case ItemType.Logs:
                returnedName = "Logs";
                break;
            case ItemType.Stone:
                returnedName = "Stone";
                break;
            case ItemType.Cobblestone:
                returnedName = "Cobblestone";
                break;
            case ItemType.Bone:
                returnedName = "Bone";
                break;
            case ItemType.Skull:
                returnedName = "Skull";
                break;
            case ItemType.Portal:
                returnedName = "Portal";
                break;
        }
        return returnedName;
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Workbench:
            case ItemType.Furnace:
            case ItemType.Cooker:
            case ItemType.Chest:
            case ItemType.Fish:
            case ItemType.Meat:
            case ItemType.Carrot:
            case ItemType.House:
            case ItemType.Logs:
            case ItemType.Stone:
            case ItemType.Cobblestone:
            case ItemType.Bone:
            case ItemType.Skull:
            case ItemType.Portal:
                return true;
            case ItemType.FishingRod:
            case ItemType.Axe:
            case ItemType.Sword:
            case ItemType.Pickaxe:
            case ItemType.Hand:
                return false;
        }
    }
}

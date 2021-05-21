using UnityEngine;

public class CookerItem : Item
{

    public GameObject fish;
    public GameObject meat;
    public GameObject fire;
    public GameObject logs;

    public ItemAttackable itemAttackableScript;

    private float cookingCounter = 0.0f;
    private int secondsAfterStartedCooking = 0;

    public enum COOKING_STATE
    {
        NOTHING,
        LOGS,
        FISH_AND_LOGS,
        MEAT
    }

    public COOKING_STATE currentState = COOKING_STATE.NOTHING;

    public void FixedUpdate()
    {
        if (currentState == COOKING_STATE.FISH_AND_LOGS)
        {
            cookingCounter += Time.deltaTime;
            secondsAfterStartedCooking = (int)(cookingCounter % 60);
        }
    }

    public override void InheritedUpdate()
    {
        if (secondsAfterStartedCooking >= 20)
        {
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Fish);
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Logs);
            itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Meat);
            AudioManager.instance.StopSound("FIRE");
            fire.gameObject.SetActive(false);
            fish.gameObject.SetActive(false);
            meat.gameObject.SetActive(true);
            currentState = COOKING_STATE.MEAT;
            cookingCounter = 0f;
            secondsAfterStartedCooking = 0;
        }
    }

    public override void InheritedInteract()
    {
        
        if (currentState == COOKING_STATE.NOTHING)
        {
            if (PlayerAttack.instance.currentWeaponItemType == InventoryItem.ItemType.Logs)
            {
                AudioManager.instance.PlaySound("PLACE_ITEM");
                logs.gameObject.SetActive(true);
                Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
                currentState = COOKING_STATE.LOGS;
                itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Logs);
            }
        }
        else if (currentState == COOKING_STATE.LOGS)
        {
            if (PlayerAttack.instance.currentWeaponItemType == InventoryItem.ItemType.Fish)
            {
                AudioManager.instance.PlaySound("PLACE_ITEM");
                AudioManager.instance.PlaySound("FIRE");
                fire.gameObject.SetActive(true);
                fish.gameObject.SetActive(true);
                Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
                currentState = COOKING_STATE.FISH_AND_LOGS;
                itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Fish);
            }
        }
        else if (currentState == COOKING_STATE.MEAT)
        {
            meat.gameObject.SetActive(false);
            logs.gameObject.SetActive(false);
            currentState = COOKING_STATE.NOTHING;
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Meat);
            Inventory.instance.AddItem(InventoryItem.ItemType.Meat, 1, true);
        }
        
    }

}

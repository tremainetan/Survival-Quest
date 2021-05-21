using UnityEngine;

public class FurnaceItem : Item
{
    public GameObject effects;
    public GameObject stone;
    public GameObject logs;
    public GameObject cobblestone;

    public ItemAttackable itemAttackableScript;

    private float cookingCounter = 0.0f;
    private int secondsAfterStartedCooking = 0;

    public enum MELTING_STATE
    {
        NOTHING,
        LOGS,
        STONES_AND_LOGS,
        COBBLESTONE
    }

    public MELTING_STATE currentState = MELTING_STATE.NOTHING;

    public void FixedUpdate()
    {
        if (currentState == MELTING_STATE.STONES_AND_LOGS)
        {
            cookingCounter += Time.deltaTime;
            secondsAfterStartedCooking = (int)(cookingCounter % 60);
        }
    }

    public override void InheritedUpdate()
    {
        if (secondsAfterStartedCooking >= 20)
        {
            AudioManager.instance.StopSound("FIRE");
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Stone);
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Logs);
            itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Cobblestone);

            stone.SetActive(false);
            logs.SetActive(false);
            cobblestone.SetActive(true);

            effects.gameObject.SetActive(false);
            currentState = MELTING_STATE.COBBLESTONE;
            cookingCounter = 0f;
            secondsAfterStartedCooking = 0;
        }
    }

    public override void InheritedInteract()
    {

        if (currentState == MELTING_STATE.NOTHING)
        {
            if (PlayerAttack.instance.currentWeaponItemType == InventoryItem.ItemType.Logs)
            {
                AudioManager.instance.PlaySound("PLACE_ITEM");
                Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
                currentState = MELTING_STATE.LOGS;
                itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Logs);
                logs.SetActive(true);
            }
        }
        else if (currentState == MELTING_STATE.LOGS)
        {
            if (PlayerAttack.instance.currentWeaponItemType == InventoryItem.ItemType.Stone)
            {
                AudioManager.instance.PlaySound("PLACE_ITEM");
                AudioManager.instance.PlaySound("FIRE");
                Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
                currentState = MELTING_STATE.STONES_AND_LOGS;
                effects.gameObject.SetActive(true);
                itemAttackableScript.itemsThatDrop.Add(InventoryItem.ItemType.Stone);
                stone.SetActive(true);
            }
        }
        else if (currentState == MELTING_STATE.COBBLESTONE)
        {
            currentState = MELTING_STATE.NOTHING;
            itemAttackableScript.itemsThatDrop.Remove(InventoryItem.ItemType.Cobblestone);
            cobblestone.SetActive(false);
            Inventory.instance.AddItem(InventoryItem.ItemType.Cobblestone, 1, true);
        }

    }

}

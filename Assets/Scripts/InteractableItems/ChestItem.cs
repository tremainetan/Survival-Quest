using System.Collections.Generic;
using UnityEngine;

public class ChestItem : Item
{

    public List<InventoryItem> items = new List<InventoryItem>();

    public ItemAttackable itemAttackableScript;

    public override void InheritedInteract()
    {
        if (!PlayerInteraction.interactingWithWorkstation)
        {
            AudioManager.instance.PlaySound("INTERACT");
            //Enable Chest UI and Toggle On Inventory
            ChestSlots.instance.currentChestOpen = this;
            PlayerMovement.chestOpen = true;
            InventoryUI.instance.ToggleInventory(true);
            ChestSlots.instance.chestUI.SetActive(true);
            ChestSlots.instance.UpdateSlots();
            PlayerInteraction.interactingWithWorkstation = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {

            //Disable Chest UI and Toggle Off Inventory
            ChestSlots.instance.currentChestOpen = null;
            PlayerMovement.chestOpen = false;
            InventoryUI.instance.ToggleInventory(false);
            ChestSlots.instance.chestUI.SetActive(false);
            PlayerInteraction.interactingWithWorkstation = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

}

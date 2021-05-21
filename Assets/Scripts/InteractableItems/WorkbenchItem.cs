using UnityEngine;
using UnityEngine.UI;

public class WorkbenchItem : Item
{

    public override void InheritedInteract()
    {
        if (!PlayerInteraction.interactingWithWorkstation)
        {
            AudioManager.instance.PlaySound("INTERACT");
            InventoryUI.instance.ToggleInventory(true);
            WorkbenchSlots.instance.workbenchUI.SetActive(true);
            PlayerInteraction.interactingWithWorkstation = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            InventoryUI.instance.ToggleInventory(false);
            WorkbenchSlots.instance.workbenchUI.SetActive(false);
            PlayerInteraction.interactingWithWorkstation = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

}

using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image[] icons;
    public GameObject unselectedItemButton;
    public GameObject selectedItemButton;
    public TextMeshProUGUI uiText;
    public InventoryItem item;

    public void Awake()
    {
        
        item = new InventoryItem(InventoryItem.ItemType.Hand, 1);
    }

    public void AddItem(InventoryItem item)
    {
        this.item = item;
        UpdateSprites();
        UpdateAmount();
    }
    public void ClearSlot()
    {
        item = new InventoryItem(InventoryItem.ItemType.Hand, 1);
        foreach (Image icon in icons) {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    public void OnClick()
    {
        if (!PlayerMovement.chestOpen)
        {
            if (Inventory.instance.currentSelectedSlot != this) AudioManager.instance.PlaySound("CLICK");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //Swap Items with The Currently Selected Slot
                if (Inventory.instance.currentSelectedSlot != this)
                {
                    InventorySlot selectedSlot = Inventory.instance.currentSelectedSlot;
                    InventoryItem selectedItem = selectedSlot.item;
                    InventoryItem clickedItem = item;
                    if (selectedItem.itemType != InventoryItem.ItemType.Hand && clickedItem.itemType != InventoryItem.ItemType.Hand)
                    {
                        int selectedItemIndex = Inventory.instance.items.IndexOf(selectedItem);
                        int clickedItemIndex = Inventory.instance.items.IndexOf(clickedItem);
                        Inventory.instance.items[selectedItemIndex] = clickedItem;
                        Inventory.instance.items[clickedItemIndex] = selectedItem;

                        InventoryUI.instance.RefreshInventoryItems();
                    }
                }
            }
            SelectSlot();
        }
        else
        {
            if (item.itemType != InventoryItem.ItemType.Hand)
            {

                //Place Item into Chest
                if (ChestSlots.instance.AddItem(item))
                {
                    AudioManager.instance.PlaySound("CLICK");
                    Inventory.instance.RemoveItem(item.itemType, item.amount);
                }
            }
        }
        

    }

    public void SelectSlot()
    {
        //Select Slot
        InventoryUI.instance.UnSelectAllSlots();
        ToggleSelect(true);
        Inventory.instance.currentSelectedSlot = this;
        PlayerAttack.instance.parsedWeaponItemType = item.itemType;
        HUD.instance.UpdateCurrentItemName();
    }

    public void UpdateSprites()
    {
        foreach (Image icon in icons)
        {
            icon.sprite = item.GetSprite();
            icon.enabled = true;
        }
    }

    public void UpdateAmount()
    {
        if (item.amount > 1) uiText.SetText(item.amount.ToString());
        else uiText.SetText("");
    }

    public void ToggleSelect(bool select)
    {
        unselectedItemButton.SetActive(!select);
        selectedItemButton.SetActive(select);
    }

    
}
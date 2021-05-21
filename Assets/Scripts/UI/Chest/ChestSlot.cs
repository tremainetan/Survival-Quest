using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestSlot : MonoBehaviour
{

    public Image icon;
    public TextMeshProUGUI uiText;
    public InventoryItem item;
    public int amount;

    public void OnClick()
    {
        if (Inventory.instance.HasSpaceFor(item.itemType, amount))
        {
            if (item != null)
            {
                AudioManager.instance.PlaySound("CLICK");
                Inventory.instance.AddItem(item.itemType, amount, false);
                ChestSlots.instance.RemoveItem(item);
            }
        }
        
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        item = null;
        uiText.SetText("");
        amount = 0;
    }

    public void UpdateItem(InventoryItem item)
    {
        this.item = item;
        
        icon.sprite = item.GetSprite();
        icon.enabled = true;

        amount = item.amount;
        if (amount > 1)
        {
            string amountString = amount.ToString();
            uiText.SetText(amountString);
        }
        else uiText.SetText("");
    }

}

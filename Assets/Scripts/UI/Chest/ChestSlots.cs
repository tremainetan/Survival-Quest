using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSlots : MonoBehaviour
{
    public static ChestSlots instance;

    public GameObject chestUI;
    public ChestItem currentChestOpen = null;

    public ChestSlot[] chestSlots;

    public int space = 8;

    private void Awake()
    {
        instance = this;
        chestUI.SetActive(false);
        chestSlots = GetComponentsInChildren<ChestSlot>();
    }

    public bool AddItem(InventoryItem item)
    {
        bool successfullyAdded = false;
        bool chestAlreadyHasItemType = false;
        foreach (InventoryItem i in currentChestOpen.items)
        {
            if (i.itemType == item.itemType) chestAlreadyHasItemType = true;
        }

        if (item.IsStackable() && chestAlreadyHasItemType)
        {
            foreach (InventoryItem i in currentChestOpen.items)
            {
                if (i.itemType == item.itemType)
                {
                    i.amount += item.amount;
                    successfullyAdded = true;
                }
            }
        }
        else
        {
            if (!(currentChestOpen.items.Count >= space))
            {
                currentChestOpen.items.Add(item);
                successfullyAdded = true;
            }
        }

        UpdateSlots();
        if (successfullyAdded && currentChestOpen.itemAttackableScript != null) currentChestOpen.itemAttackableScript.itemsThatDrop.Add(item.itemType);

        return successfullyAdded;
    }

    public void RemoveItem(InventoryItem item)
    {
        currentChestOpen.items.Remove(item);
        UpdateSlots();
        if (currentChestOpen.itemAttackableScript != null) currentChestOpen.itemAttackableScript.itemsThatDrop.Remove(item.itemType);
    }

    public void UpdateSlots()
    {
        foreach (ChestSlot slot in chestSlots)
        {
            slot.ClearSlot();
        }

        foreach (InventoryItem item in currentChestOpen.items)
        {
            int index = currentChestOpen.items.IndexOf(item);
            chestSlots[index].UpdateItem(item);
        }
    }

}

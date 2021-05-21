using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public GameObject inventoryUI;

    [SerializeField] private Transform inventorySlots;
    private Inventory inventory;
    private InventorySlot[] slots;

    private void Awake()
    {
        instance = this;
        slots = GetComponentsInChildren<InventorySlot>();
    }

    private void Start()
    {
        inventory = Inventory.instance;
        RefreshInventoryItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !StateHandler.instance.gamePaused)
        {
            InventoryItem.ItemType droppedItemType = Inventory.instance.currentSelectedSlot.item.itemType;

            if (droppedItemType != InventoryItem.ItemType.Hand)
            {
                AudioManager.instance.PlaySound("POP");
                Inventory.instance.RemoveItem(Inventory.instance.currentSelectedSlot.item.itemType, 1);
                GameObject droppedItem = DroppedItemPrefabs.instance.GetObject(droppedItemType);
                Vector3 playerTransform = PlayerMovement.instance.droppedItemTransform.position;
                Instantiate(
                    droppedItem,
                    new Vector3(playerTransform.x, droppedItem.transform.position.y, playerTransform.z),
                    droppedItem.transform.rotation
                );
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !PlayerInteraction.interactingWithWorkstation && !StateHandler.instance.gamePaused)
        {
            RectTransform inventoryTransform = inventoryUI.GetComponent<RectTransform>();
            if (inventoryTransform.anchoredPosition.x == -100) ToggleInventory(false);
            else ToggleInventory(true);
            RefreshInventoryItems();
        }
    }

    public void ToggleInventory(bool on)
    {
        RectTransform inventoryTransform = inventoryUI.GetComponent<RectTransform>();
        if (!on)
        {
            SetInventoryTransformX(inventoryTransform, false);
            PlayerMovement.inventoryActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            SetInventoryTransformX(inventoryTransform, true);
            PlayerMovement.inventoryActive = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void UnSelectAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            slot.ToggleSelect(false);
        }
    }

    public void RefreshInventoryItems()
    {
        if (inventory != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else slots[i].ClearSlot();

                slots[i].UpdateAmount();
            }
            Inventory.instance.currentSelectedSlot.SelectSlot();
        }
    }

    private void SetInventoryTransformX(RectTransform inventoryTransform, bool appear)
    {
        if (appear)
            inventoryTransform.anchoredPosition = new Vector2(-100f, inventoryTransform.anchoredPosition.y);
        else inventoryTransform.anchoredPosition = new Vector2(500f, inventoryTransform.anchoredPosition.y);
    }

}

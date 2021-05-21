using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public static Inventory instance;
	public InventoryUI inventoryUI;

	public List<InventoryItem> items;
	public InventorySlot currentSelectedSlot;
	public int space = 12;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		inventoryUI = InventoryUI.instance;
		if (items == null) items = new List<InventoryItem>();
	}

	public void ParseInventory(List<InventoryItem> inventory)
	{
		items = inventory;
		InventoryUI.instance.RefreshInventoryItems();

	}

	public void RemoveItem(InventoryItem.ItemType itemType, int amount)
	{
		if (itemType != InventoryItem.ItemType.Hand)
		{
			foreach (InventoryItem item in items)
			{
				if (item.itemType == itemType)
				{
					int remainingAmount = item.amount - amount;
					if (remainingAmount > 0) item.amount -= amount;
					else if (remainingAmount == 0) items.Remove(item);
					break;
				}
			}
			InventoryUI.instance.RefreshInventoryItems();
		}
	}

	public void AddItem(InventoryItem.ItemType itemType, int amount, bool playSound)
	{
		InventoryItem item = new InventoryItem(itemType, amount);

		bool inventoryAlreadyHasItemType = false;
		foreach (InventoryItem i in items)
		{
			if (i.itemType == itemType) inventoryAlreadyHasItemType = true;
		}

		if (item.IsStackable() && inventoryAlreadyHasItemType)
		{
			if (playSound) AudioManager.instance.PlaySound("POP");
			foreach (InventoryItem inventoryItem in items)
			{
				if (inventoryItem.itemType == item.itemType) inventoryItem.amount += item.amount;
			}
		}
		else
		{
			if (!(items.Count >= space))
			{
				if (playSound) AudioManager.instance.PlaySound("POP");
				items.Add(item);
			}
			else
			{
				GameObject droppedItem = DroppedItemPrefabs.instance.GetObject(itemType);
				Vector3 playerTransform = PlayerMovement.instance.droppedItemTransform.position;
				Instantiate(
					droppedItem,
					new Vector3(playerTransform.x, droppedItem.transform.position.y, playerTransform.z),
					droppedItem.transform.rotation
				);
			}
		}
		InventoryUI.instance.RefreshInventoryItems();
	}

	public bool HasSpaceFor(InventoryItem.ItemType itemType, int amount)
	{
		bool hasSpace = false;

		InventoryItem item = new InventoryItem(itemType, amount);

		bool inventoryAlreadyHasItemType = false;
		foreach (InventoryItem i in items)
		{
			if (i.itemType == itemType) inventoryAlreadyHasItemType = true;
		}

		if (item.IsStackable() && inventoryAlreadyHasItemType)
		{
			foreach (InventoryItem inventoryItem in items)
			{
				if (inventoryItem.itemType == item.itemType) hasSpace = true;
			}
		}
		else if (!(items.Count >= space)) hasSpace = true;

		return hasSpace;
	}

}

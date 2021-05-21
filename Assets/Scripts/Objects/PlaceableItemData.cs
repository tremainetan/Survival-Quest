using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaceableItemData
{
	[Serializable]
	public class ChestData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;

		public List<InventoryItem> chestItems;
		public List<InventoryItem.ItemType> itemsThatDrop;

		public ChestData(GameObject obj)
		{
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
			chestItems = obj.GetComponent<ChestItem>().items;
			itemsThatDrop = obj.GetComponent<ItemAttackable>().itemsThatDrop;
		}
		
		public void PlaceObject(GameObject returnedObject)
        {
			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
			returnedObject.GetComponent<ChestItem>().items = chestItems;
			returnedObject.GetComponent<ItemAttackable>().itemsThatDrop = itemsThatDrop;

			GameHandler.instance.PlaceOverworldObject(returnedObject);
        }

	}

	[Serializable]
	public class CookerData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;
		public CookerItem.COOKING_STATE cookingState;
		public List<InventoryItem.ItemType> itemsThatDrop = new List<InventoryItem.ItemType>();
		public bool bonFireActive;
		public bool meatActive;

		public CookerData(GameObject obj)
		{
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
			cookingState = obj.GetComponent<CookerItem>().currentState;

			itemsThatDrop.Add(InventoryItem.ItemType.Cooker);

			//Assume finish cooking
			if (cookingState == CookerItem.COOKING_STATE.FISH_AND_LOGS) cookingState = CookerItem.COOKING_STATE.MEAT;

            switch (cookingState)
            {
                case CookerItem.COOKING_STATE.NOTHING:
                    bonFireActive = false;
                    meatActive = false;
                    break;
                case CookerItem.COOKING_STATE.MEAT:
                    bonFireActive = false;
                    meatActive = true;
                    itemsThatDrop.Add(InventoryItem.ItemType.Meat);
                    break;
                case CookerItem.COOKING_STATE.LOGS:
                    bonFireActive = true;
                    meatActive = false;
                    itemsThatDrop.Add(InventoryItem.ItemType.Logs);
                    break;
            }

        }

		public void PlaceObject(GameObject returnedObject)
		{
			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
			if (bonFireActive) returnedObject.transform.Find("Bonfire").gameObject.SetActive(true);
			if (meatActive) returnedObject.transform.Find("Meat").gameObject.SetActive(true);
			returnedObject.GetComponent<CookerItem>().currentState = cookingState;
			returnedObject.GetComponent<ItemAttackable>().itemsThatDrop = itemsThatDrop;

			GameHandler.instance.PlaceOverworldObject(returnedObject);
		}

	}

	[Serializable]
	public class FurnaceData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;
		public FurnaceItem.MELTING_STATE meltingState;
		public List<InventoryItem.ItemType> itemsThatDrop = new List<InventoryItem.ItemType>();
		public bool logsActive;
		public bool cobblestoneActive;

		public FurnaceData(GameObject obj)
		{
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
			meltingState = obj.GetComponent<FurnaceItem>().currentState;

			itemsThatDrop.Add(InventoryItem.ItemType.Furnace);

			//Assume finish melting
			if (meltingState == FurnaceItem.MELTING_STATE.STONES_AND_LOGS) meltingState = FurnaceItem.MELTING_STATE.COBBLESTONE;

			switch (meltingState)
            {
				case FurnaceItem.MELTING_STATE.NOTHING:
					logsActive = false;
					cobblestoneActive = false;
					break;
				case FurnaceItem.MELTING_STATE.COBBLESTONE:
					logsActive = false;
					cobblestoneActive = true;
					itemsThatDrop.Add(InventoryItem.ItemType.Cobblestone);
					break;
				case FurnaceItem.MELTING_STATE.LOGS:
					logsActive = true;
					cobblestoneActive = false;
					itemsThatDrop.Add(InventoryItem.ItemType.Logs);
					break;
			}
			
		}

		public void PlaceObject(GameObject returnedObject)
		{

			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
			if (logsActive) returnedObject.transform.Find("Logs").gameObject.SetActive(true);
			if (cobblestoneActive) returnedObject.transform.Find("Cobblestone").gameObject.SetActive(true);
			returnedObject.GetComponent<FurnaceItem>().currentState = meltingState;
			returnedObject.GetComponent<ItemAttackable>().itemsThatDrop = itemsThatDrop;

			GameHandler.instance.PlaceOverworldObject(returnedObject);

		}

	}

	[Serializable]
	public class HouseData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;

		public HouseData(GameObject obj)
        {
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
		}

		public void PlaceObject(GameObject returnedObject)
        {
			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));

			GameHandler.instance.PlaceOverworldObject(returnedObject);
		}

    }

	[Serializable]
	public class PortalData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;
		public PortalData(GameObject obj)
        {
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
		}

		public void PlaceObject(GameObject returnedObject)
		{
			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));

			GameHandler.instance.PlaceOverworldObject(returnedObject);
		}
	}

	[Serializable]
	public class WorkbenchData
	{

		public float posX;
		public float posY;
		public float posZ;

		public float rotX;
		public float rotY;
		public float rotZ;

		public WorkbenchData(GameObject obj)
        {
			posX = obj.transform.position.x;
			posY = obj.transform.position.y;
			posZ = obj.transform.position.z;
			rotX = obj.transform.rotation.x;
			rotY = obj.transform.rotation.y;
			rotZ = obj.transform.rotation.z;
		}
		public void PlaceObject(GameObject returnedObject)
		{
			returnedObject.transform.position = new Vector3(posX, posY, posZ);
			returnedObject.transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));

			GameHandler.instance.PlaceOverworldObject(returnedObject);
		}

	}

}

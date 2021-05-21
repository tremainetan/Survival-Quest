using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Placeable : MonoBehaviour
{
    public bool failed = false;
    public bool destroyed = false;
    public bool placed = false;

    private float placedCounter = 0.0f;

    public GameObject objectToDestroyWhenFailed;

    public InventoryItem.ItemType droppedItemWhenFailed;

    private void FixedUpdate()
    {
        if (!placed) placedCounter += Time.deltaTime;
    }

    private void Update()
    {
        if (placedCounter >= 0.5f) placed = true;

        if (failed && !destroyed && !placed)
        {
            destroyed = true;
            GameObject droppedItem = DroppedItemPrefabs.instance.GetObject(droppedItemWhenFailed);
            Vector3 position = new Vector3(transform.position.x, droppedItem.transform.position.y, transform.position.z);
            Quaternion rotation = droppedItem.transform.rotation;
            Instantiate(droppedItem, position, rotation);
            if (objectToDestroyWhenFailed == null) objectToDestroyWhenFailed = gameObject;
            Destroy(objectToDestroyWhenFailed);
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Ground"))
        {
            failed = true;
        }
        
    }

}

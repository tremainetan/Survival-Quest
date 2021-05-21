using UnityEngine;

public class DroppedItem: Item
{
    public InventoryItem.ItemType itemType;
    public int amount = 1;
    public float jumpSpeed;

    public DroppedItem(InventoryItem.ItemType itemType, int amount)
    {
        this.radius = 1f;
        this.itemType = itemType;
        this.amount = amount;
    }
    public override void Touch()
    {
        if (Inventory.instance.HasSpaceFor(itemType, amount)) {
            Inventory.instance.AddItem(itemType, amount, true);
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed);
        }
        else
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }



}

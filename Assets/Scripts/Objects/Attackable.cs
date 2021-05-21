using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{

    public float radius = 1f;
    private Transform playerTransform;
    public Transform attackableTransform;
    public bool touching = false;
    public int health;

    //Variables to be overwritten
    public HealthBar healthBar;

    public string hitSoundName;
    public int maxHealth;
    public bool dropAtPlayer;
    public List<InventoryItem.ItemType> weaponsThatDamage;
    public List<InventoryItem.ItemType> itemsThatDrop;

    private void Start()
    {
        playerTransform = PlayerMovement.instance.transform;
        if (attackableTransform == null) attackableTransform = transform;
        if (healthBar == null) healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }
    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, attackableTransform.position);
        if (distance <= radius) touching = true;
        else touching = false;

        if (health <= 0) DropSelf();

        InheritedUpdate();
    }

    public bool AttackMe()
    {
        bool canDamage = false;
        if (touching)
        {
            foreach (InventoryItem.ItemType type in weaponsThatDamage)
            {
                if (type == PlayerAttack.instance.currentWeaponItemType) canDamage = true;
            }
        }

        if (canDamage)
        {
            AudioManager.instance.PlaySound(hitSoundName);
            TakeDamage();
        }
        return canDamage;
    }
    public virtual void DropSelf()
    {
        Transform dropLocation;
        if (dropAtPlayer) dropLocation = PlayerMovement.instance.droppedItemTransform;
        else dropLocation = attackableTransform;

        foreach (InventoryItem.ItemType item in itemsThatDrop)
        {
            GameObject droppedItem = DroppedItemPrefabs.instance.GetObject(item);
            Vector3 position = dropLocation.position;
            Quaternion rotation = droppedItem.transform.rotation;
            Instantiate(droppedItem, position, rotation);
        }


        PlayerAttack.instance.UpdateNavMesh();
    }

    public virtual void InheritedUpdate()
    {

    }

    public virtual void TakeDamage()
    {
        if (!healthBar.gameObject.activeSelf) healthBar.gameObject.SetActive(true);
        healthBar.SetHealth(health);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackableTransform == null) attackableTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackableTransform.position, radius);
    }

}

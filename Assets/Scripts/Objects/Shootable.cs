using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public int health;

    //Variables to be overwritten
    public HealthBar healthBar;

    public string hitSoundName;
    public int maxHealth;

    private void Start()
    {
        if (healthBar == null) healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }
    private void Update()
    {

        if (health <= 0) DropSelf();

        InheritedUpdate();
    }

    public void BulletHit(Vector3 hitPoint)
    {
        InheritedBulletHit(hitPoint);
        AudioManager.instance.PlaySound(hitSoundName);
        TakeDamage();
    }

    public virtual void InheritedBulletHit(Vector3 hitPoint)
    {

    }

    public virtual void DropSelf()
    {
    }

    public virtual void InheritedUpdate()
    {

    }

    public virtual void TakeDamage()
    {
        if (!healthBar.gameObject.activeSelf) healthBar.gameObject.SetActive(true);
        healthBar.SetHealth(health);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureAttackable : Attackable
{
    private bool dead = false;

    private float timerCounter = 0.0f;
    private int secondsAfterDeath = 0;
    private void FixedUpdate()
    {
        if (dead)
        {
            timerCounter += Time.deltaTime;
            secondsAfterDeath = (int)(timerCounter % 60);
        }
    }
    public override void DropSelf()
    {
        if (!dead)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            base.DropSelf();
            healthBar.gameObject.SetActive(false);
            dead = true;
        }
    }
    public override void InheritedUpdate()
    {
        if (secondsAfterDeath >= 59)
        {
            health = maxHealth;
            dead = false;
            timerCounter = 0.0f;
            secondsAfterDeath = 0;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;
            PlayerAttack.instance.navMesh.BuildNavMesh();
            healthBar.SetHealth(maxHealth);
        }

    }

    public override void TakeDamage()
    {
        health -= 1;
        base.TakeDamage();
    }

}

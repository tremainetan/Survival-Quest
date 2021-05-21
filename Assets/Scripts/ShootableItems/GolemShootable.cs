using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemShootable : Shootable
{
    public GameObject bulletHitEffect;

    public override void InheritedBulletHit(Vector3 hitPoint)
    {
        Instantiate(bulletHitEffect, hitPoint, Quaternion.identity);
    }

    public override void TakeDamage()
    {
        health--;
        base.TakeDamage();

    }

    public override void DropSelf() {

        if (BossPlatformController.instance.golems.Count == 1)
        {
            BossPlatformController.instance.shouldRise = true;
            BossPlatformController.instance.underworldSkeletonSpawn.SetActive(true);
            BossPlatformController.instance.GetComponentInChildren<BossColorController>().enabled = true;
        }


        Destroy(gameObject);
        Destroy(this);

    }

}

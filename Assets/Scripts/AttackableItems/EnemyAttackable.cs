using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackable : Attackable
{
    public GameObject itemToDestroy;

    public override void DropSelf()
    {
        PlayerStats.instance.skeletonKills++;
        base.DropSelf();
        AudioManager.instance.PlaySound("DESTROY_ENEMY");
        Destroy(itemToDestroy);
    }

    public override void TakeDamage()
    {
        health -= 1;
        base.TakeDamage();
    }



}

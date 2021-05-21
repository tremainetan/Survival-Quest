using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackable : Attackable
{
    public GameObject itemToDestroy;

    public override void DropSelf()
    {
        base.DropSelf();
        Destroy(itemToDestroy);
    }

    public override void TakeDamage()
    {
        health -= 1;
        base.TakeDamage();
    }



}

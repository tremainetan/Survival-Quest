using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootable : Shootable
{

    public GameObject bulletHitEffect;
    public GameObject explosionEffect;
    public GameObject terrain;
    public GameObject kneelingStatues;
    public Portal underworldPortal;
    public Material greenMaterial;

    public Transform explosionTransform;

    public override void InheritedBulletHit(Vector3 hitPoint)
    {
        Instantiate(bulletHitEffect, hitPoint, Quaternion.identity);
    }

    public override void TakeDamage()
    {
        if (BossPlatformController.instance.risen)
        {
            health--;
            base.TakeDamage();
        }

    }

    public override void DropSelf()
    {

        BossKilled();

        Destroy(gameObject);
        Destroy(this);

    }

    private void BossKilled()
    {

        //Add to Player Stats
        PlayerStats.instance.bossKills++;

        //Explode
        Instantiate(explosionEffect, explosionTransform.position, explosionTransform.rotation);

        //Move Boss Down
        BossPlatformController.instance.shouldRise = false;

        //Disable Skeleton Spawner
        UnderworldSkeletonSpawn.instance.DisableSpawner();

        //Change Red Glows on Terrain to Green
        Material[] materials = terrain.GetComponent<MeshRenderer>().materials;
        materials[3] = greenMaterial;
        terrain.GetComponent<MeshRenderer>().materials = materials;

        //Enable Kneeling Statues
        kneelingStatues.SetActive(true);

        //Activate Portal
        underworldPortal.canEnter = true;

    }

}

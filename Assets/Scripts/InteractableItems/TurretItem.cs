using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItem : Item
{

    public Transform playerSitPosition;
    public Transform playerStandPosition;
    public Transform turntableTransform; //Horizontal Movement
    public Transform gunTransform; //Vertical Movement

    public ParticleSystem muzzleFlash;


    public static TurretItem instance;

    private void Awake()
    {
        instance = this;
    }

    public override void InheritedInteract()
    {
        PlayerMovement.instance.MountTurret();
    }

    public override void InheritedUpdate()
    {
        base.InheritedUpdate();
        PlayerInteraction.instance.touchingTurret = touching;
        if (PlayerMovement.instance.mountedToTurret)
        {
            //Player Mounted
            turntableTransform.rotation = PlayerMovement.instance.transform.rotation;
            gunTransform.localRotation = Camera.main.transform.localRotation;
        }
        else
        {
            //Player Not Mounted
            turntableTransform.rotation = Quaternion.identity;
            gunTransform.rotation = Quaternion.identity;
        }
    }

}

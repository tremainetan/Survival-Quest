using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public bool canEnter = false;
    public GameHandler.WORLD travellingTowards;

    private void Start()
    {
        if (travellingTowards == GameHandler.WORLD.UNDERWORLD) canEnter = true;
        else canEnter = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && canEnter)
        {
            if (travellingTowards == GameHandler.WORLD.UNDERWORLD) LevelManager.instance.MoveToUnderworld();
            else LevelManager.instance.MoveToOverworld();
        }
    }

}

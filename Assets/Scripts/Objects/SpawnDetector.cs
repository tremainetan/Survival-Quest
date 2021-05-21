using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDetector : MonoBehaviour
{

    public GameHandler.WORLD world;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (world == GameHandler.WORLD.OVERWORLD) GameHandler.instance.LoadOverworld();
            else GameHandler.instance.LoadUnderworld();
            GameHandler.instance.currentWorld = world;
        }
    }

}

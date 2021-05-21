using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOfDeath : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats.instance.Die();
        }
    }

}

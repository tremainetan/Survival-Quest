using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSign : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{

    public float duration;
    private float durationCounter;

    private void Update()
    {
        durationCounter += Time.deltaTime;
        if (durationCounter > duration)
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
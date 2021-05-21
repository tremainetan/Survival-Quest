using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatformController : MonoBehaviour
{

    public static BossPlatformController instance;

    public List<GameObject> golems;
    public GameObject underworldSkeletonSpawn;

    public Vector3 higherPosition;
    public Vector3 lowerPosition;

    public float speed;

    public bool shouldRise = false;
    public bool risen = false;

    private void Awake()
    {
        instance = this;
        transform.position = lowerPosition;
    }

    private void Update()
    {

        golems.RemoveAll(golem => golem == null);

        if (shouldRise && !risen)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, higherPosition, step);
        }

        if (!shouldRise && risen)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lowerPosition, step);
        }

        if (transform.position == higherPosition && !risen) risen = true;
        else if (transform.position == lowerPosition && risen) risen = false;

    }
}

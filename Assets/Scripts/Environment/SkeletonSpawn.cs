using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawn : MonoBehaviour
{

    public static SkeletonSpawn instance;

    public List<GameObject> skeletons = new List<GameObject>();
    public GameObject skeletonPrefab;
    public GameObject caveEyes;
    public Transform skeletonSpawn;
    public Transform sunTransform;
    public Transform firstWalkPoint;

    public bool night = false;
    public float spawnInterval = 3f;

    private float spawnCounter = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

        if (sunTransform.position.y <= 0) night = true;
        else night = false;

        if (night) caveEyes.SetActive(true);
        else caveEyes.SetActive(false);

        if (night)
        {
            spawnCounter += Time.deltaTime;
            if (spawnCounter >= spawnInterval)
            {
                spawnCounter = 0f;
                SpawnSkeleton(1);
            }
        }
        else
        {
            spawnCounter = 0f;
        }

    }

    private void SpawnSkeleton(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(skeletonSpawn.position.x, skeletonPrefab.transform.position.y, skeletonSpawn.position.z);
            GameObject skeleton = Instantiate(skeletonPrefab, position, skeletonPrefab.transform.rotation);
            skeleton.GetComponent<SkeletonController>().walkPoint = firstWalkPoint.position;
        }
    }
}

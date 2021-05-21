using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldSkeletonSpawn : MonoBehaviour
{

    public static UnderworldSkeletonSpawn instance;

    public List<GameObject> skeletons = new List<GameObject>();
    public GameObject skeletonPrefab;

    public float spawnInterval = 3f;

    private float spawnCounter = 0f;
    private bool shouldSpawn = true;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnInterval)
        {
            spawnCounter = 0f;
            if (shouldSpawn) SpawnSkeleton(1);
        }

    }

    private void SpawnSkeleton(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(transform.position.x, skeletonPrefab.transform.position.y, transform.position.z);
            GameObject skeleton = Instantiate(skeletonPrefab, position, skeletonPrefab.transform.rotation);
            skeletons.Add(skeleton);
        }
    }

    public void DisableSpawner()
    {
        shouldSpawn = false;
        foreach (GameObject skeleton in skeletons) Destroy(skeleton);
        gameObject.SetActive(false);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{

    public GameObject body;
    public GameObject plasmaBallPrefab;
    public Transform plasmaBallSpawn;

    private float bulletCounter = 0f;
    public float bulletInterval;

    private void Update()
    {

        float posX = PlayerMovement.instance.transform.position.x;
        float posY = body.transform.position.y;
        float posZ = PlayerMovement.instance.transform.position.z;
        Vector3 targetPos = new Vector3(posX, posY, posZ);
        body.transform.LookAt(targetPos);

        bulletCounter += Time.deltaTime;

        if (bulletCounter >= bulletInterval)
        {
            bulletCounter = 0f;
            GameObject inst = Instantiate(plasmaBallPrefab, plasmaBallSpawn.position, plasmaBallSpawn.rotation);
            inst.transform.parent = gameObject.transform;
        }

    }
}

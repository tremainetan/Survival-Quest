using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallController : MonoBehaviour
{

    private Vector3 direction;
    public float speed;

    private void Start()
    {
        direction = (Camera.main.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;

        if (transform.position.z <= -100) Kill();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) PlayerStats.instance.TakeDamage(2);
        Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
        Destroy(this);
    }

}

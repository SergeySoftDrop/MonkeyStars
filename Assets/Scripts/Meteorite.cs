using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public GameObject DustParticle;

    private Vector3 randomDirection;

    void Start()
    {
        randomDirection = Random.insideUnitSphere;
    }

    void Update()
    {
        transform.Rotate(randomDirection * rotationSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(DustParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    public Transform target;
    float speed = 2;

    void Start()
    {
        speed *= Random.Range(0.5f, 2);    
    }

    void Update()
    {
        if(target != null)
        {
            transform.LookAt(target);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject g = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(g, 2);
        Destroy(gameObject);
    }
}
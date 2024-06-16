using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;

    public Transform target;
    public GameConfig gameConfig;

    private float speed;

    void Start()
    {
        speed = Random.Range(gameConfig.Enemy_1SpeedMin, gameConfig.Enemy_1SpeedMax);
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
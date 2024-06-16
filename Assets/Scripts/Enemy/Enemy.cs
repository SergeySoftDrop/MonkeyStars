using Assets.Scripts.Conf.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;

    public Transform target;
    public GameConfig gameConfig;

    private float speed;
    private float HP;

    public event Action OnExplosion;

    void Start()
    {
        speed = UnityEngine.Random.Range(gameConfig.Enemy_1SpeedMin, gameConfig.Enemy_1SpeedMax);
        HP = gameConfig.Enemy_1HP;
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
        if(other.gameObject.CompareTag("Bullet"))
        {
            HP -= other.gameObject.GetComponent<Bullet>().Damage();
        } 
        else if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy_2") || other.gameObject.CompareTag("PlayerBase"))
        {
            HP -= HP;
        }
        else
        {
            HP--;
        }

        if(HP <= 0)
        {
            OnExplosion?.Invoke();
            GameObject g = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(gameObject);
        }
    }
}
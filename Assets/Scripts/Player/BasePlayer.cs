using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public GameConfig gameConfig;

    public GameObject baseExplosionPref;

    public float health = 10;

    public event Action OnDestroy;
    public event Action onDamage;

    private void Start()
    {
        health = gameConfig.BaseHP;
    }

    void OnTriggerEnter(Collider other)
    {
        health -= 1;
        onDamage?.Invoke();
        if (health <= 0)
        {
            GameObject g = Instantiate(baseExplosionPref, transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(gameObject);
            OnDestroy?.Invoke();
        }
    }
}
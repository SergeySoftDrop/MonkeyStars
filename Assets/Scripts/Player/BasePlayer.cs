using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public GameConfig gameConfig;

    public GameObject baseExplosionPref;

    public int health = 10;

    private void Start()
    {
        health = gameConfig.BaseHP;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Meteorite") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            health -= 1;
            if (health <= 0)
            {
                GameObject g = Instantiate(baseExplosionPref, transform.position, Quaternion.identity);
                Destroy(g, 2);
                Destroy(gameObject);
            }
        }
    }
}
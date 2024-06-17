using Assets.Scripts.Conf.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meteorite : MonoBehaviour
{
    public GameConfig gameConfig;
    public GameObject DustParticle;

    private Vector3 randomDirection;

    public event Action OnDestroy;

    void Start()
    {
        randomDirection = UnityEngine.Random.insideUnitSphere;
    }

    void Update()
    {
        transform.Rotate(randomDirection * gameConfig.MeteorRotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Meteorite hit by {other.gameObject.tag}");
        GameObject g = Instantiate(DustParticle, transform.position, Quaternion.identity);
        Destroy(g, 2);
        Destroy(gameObject);
        OnDestroy.Invoke();
    }
}

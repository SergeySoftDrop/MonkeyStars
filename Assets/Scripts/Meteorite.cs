using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameConfig gameConfig;
    public GameObject DustParticle;

    private Vector3 randomDirection;

    void Start()
    {
        randomDirection = Random.insideUnitSphere;
    }

    void Update()
    {
        transform.Rotate(randomDirection * gameConfig.MeteorRotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject g = Instantiate(DustParticle, transform.position, Quaternion.identity);
        Destroy(g, 2);
        Destroy(gameObject);
    }
}

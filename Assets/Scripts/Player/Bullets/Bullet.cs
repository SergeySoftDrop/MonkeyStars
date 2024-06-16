using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameConfig gameConfig;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > gameConfig.BulletLifeTimeInSecond) Destroy(gameObject);

        transform.Translate(Vector3.forward * GetSpeed() * Time.deltaTime);
    }

    protected virtual float GetSpeed()
    {
        return gameConfig.BulletSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
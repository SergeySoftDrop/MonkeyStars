using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public int health = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Meteorite") || other.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
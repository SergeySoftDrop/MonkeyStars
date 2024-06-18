using Assets.Scripts.Conf.Scripts;
using Assets.Scripts.Player;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameConfig gameConfig;
    public ShipModel shipModel;
    public InterfaceController interfaceController;

    public GameObject basePlayer;
    public GameObject player;

    public GameObject bulletPrefab;
    public GameObject explosionPref;

    public AudioSource shootSound;

    public event Action onDamage;
    public event Action onDestroy;

    public float HP;

    void Start()
    {
        HP = gameConfig.PlayerHP;
        Respawn();
    }

    void Update()
    {
        ResetShipPos();
        Rotate();
        Move();
    }

    private void ResetShipPos()
    {
        if(shipModel.transform.localPosition != Vector3.zero) shipModel.transform.localPosition = Vector3.zero;
        if(shipModel.transform.localRotation != Quaternion.identity)  shipModel.transform.localRotation = Quaternion.identity;
    }

    void Rotate()
    {
        float yAxis = interfaceController.RotateJoystick.Horizontal();
        float xAxis = interfaceController.MoveJoystick.Vertical();
        float zAxis = interfaceController.MoveJoystick.Horizontal();

        if (zAxis == 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                zAxis = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                zAxis = -1;
            }
        }

        if (xAxis == 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                xAxis = -1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                xAxis = 1;
            }
        }

        if (yAxis == 0)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                yAxis = -1;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                yAxis = 1;
            }
        }

        Vector3 allAxis = new Vector3(xAxis, yAxis, zAxis) * gameConfig.TransformRotationSpeed * Time.deltaTime;
        Quaternion newRotation = Quaternion.Euler(allAxis);
        player.transform.rotation *= newRotation;
    }

    void Move()
    {
        player.transform.Translate(Vector3.forward * (gameConfig.PlayerSpeed + (shipModel.boost ? gameConfig.PlayerBoost : 0)) * Time.deltaTime);
    }


    public void Shoot()
    {
        List<Vector3> firePositions = new List<Vector3>
        {
            shipModel.GunFirstTransform.position, 
            shipModel.GunSecondTransform.position, 
            shipModel.GunThirdTransform.position, 
            shipModel.GunFourthTransform.position,
        };

        foreach(var position in firePositions)
        {
            Instantiate(bulletPrefab, position + shipModel.transform.forward * 2, transform.rotation);
        }

        shootSound.Play();
    }

    void Respawn()
    {
        Collider basePlayerCollider = basePlayer.GetComponent<Collider>();

        float basePlayerRadius = 0f;
        if(basePlayerCollider != null) basePlayerRadius = basePlayerCollider.bounds.extents.magnitude;

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * gameConfig.PlayerSpwnRadius;
        Vector3 randomPosition = basePlayer.transform.position + randomDirection;

        randomPosition += randomDirection.normalized * basePlayerRadius;
        randomPosition.y = basePlayer.transform.position.y;

        transform.position = randomPosition;
        transform.LookAt(basePlayer.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(HP <= 0) return;

        onDamage?.Invoke();
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            HP -= other.gameObject.GetComponent<EnemyBullet>().Damage();
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy_2") || other.gameObject.CompareTag("PlayerBase") || other.gameObject.CompareTag("Meteorite"))
        {
            if (other.gameObject.CompareTag("PlayerBase")) Respawn();
            HP -= gameConfig.PlayerCrashDamage;
        }
        else
        {
            HP--;
        }

        if(HP <= 0)
        {
            GameObject g = Instantiate(explosionPref, shipModel.transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(shipModel);
            onDestroy?.Invoke();
        }
    }
}
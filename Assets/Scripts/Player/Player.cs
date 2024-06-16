using Assets.Scripts.Conf.Scripts;
using Assets.Scripts.Player;
using System.Collections;
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

    void Start()
    {
        Respawn();
    }

    void Update()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        float yAxis = interfaceController.RotateJoystick.Horizontal();
        float xAxis = interfaceController.MoveJoystick.Vertical();
        float zAxis = interfaceController.MoveJoystick.Horizontal();

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

        foreach (var position in firePositions)
        {
            Instantiate(bulletPrefab, position + shipModel.ship.transform.forward * 2, transform.rotation);
        }
    }

    void Respawn()
    {
        Collider basePlayerCollider = basePlayer.GetComponent<Collider>();

        float basePlayerRadius = 0f;
        if(basePlayerCollider != null) basePlayerRadius = basePlayerCollider.bounds.extents.magnitude;

        Vector3 randomDirection = Random.insideUnitSphere * gameConfig.PlayerSpwnRadius;
        Vector3 randomPosition = basePlayer.transform.position + randomDirection;

        randomPosition += randomDirection.normalized * basePlayerRadius;
        randomPosition.y = basePlayer.transform.position.y;

        transform.position = randomPosition;
        transform.LookAt(basePlayer.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerBase"))
        {
            shipModel.HP--;
            Respawn();
        }
    }
}
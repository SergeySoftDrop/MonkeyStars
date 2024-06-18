using Assets.Scripts.Conf.Scripts;
using System;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public GameConfig gameConfig;

    public GameObject player;
    public GameObject playerBase;

    public Transform[] shootPositions;

    public GameObject bulletPref;
    public GameObject explosionPref;

    public AudioSource shootsound;

    private GameObject currTarget;
    private float currTargetDistance;
    private float HP;
    private float speed;

    private bool shootIsReloading = false;
    private float shootReloadingTimer = 0;

    private int shotCounter = 0;

    public event Action OnExplosion;

    private void Start()
    {
        speed = UnityEngine.Random.Range(gameConfig.Enemy_2SpeedMin, gameConfig.Enemy_2SpeedMax);
        HP = gameConfig.Enemy_2HP;
        FindNearestTarget();
    }

    private void Update()
    {
        FollowTarget();
        Shoot();
    }

    private void FollowTarget()
    {
        FindNearestTarget();
        RotateTowards(currTarget.transform.position);
        MoveTowards(currTarget.transform.position);
    }

    private void FindNearestTarget()
    {
        float distanceToPlayer = float.MaxValue;
        float distanceToPlayerBase = float.MaxValue;

        if (player)
        {
            distanceToPlayer = GetDistance(player.transform);
        }

        if (playerBase)
        {
            distanceToPlayerBase = GetDistance(playerBase.transform);
        }

        if (distanceToPlayer < distanceToPlayerBase)
        {
            currTargetDistance = distanceToPlayer;
            currTarget = player;
        }
        else
        {
            currTargetDistance = distanceToPlayerBase;
            currTarget = playerBase;
        }
    }

    private float GetDistance(Transform position)
    {
        return Vector3.Distance(position.position, transform.position);
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, gameConfig.Enemy_2RotateSens * Time.deltaTime);
    }

    private void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void Shoot()
    {
        if (shootIsReloading)
        {
            shootReloadingTimer += Time.deltaTime;

            if (shootReloadingTimer >= gameConfig.Enemy_2ShootReloadInSecond)
            {
                shootIsReloading = false;
                shootReloadingTimer = 0;
            }
            else
            {
                return;
            }
        }

        if (shotCounter >= gameConfig.Enemy_2Shootcount)
        {
            shotCounter = 0;
            shootIsReloading = true;
            return;
        }

        if (shootReloadingTimer < 1 / gameConfig.Enemy_2ShootRateInSecond)
        {
            shootReloadingTimer += Time.deltaTime;
            return;
        }

        shootReloadingTimer = 0;

        if (currTargetDistance <= gameConfig.Enemy_2ShootDistance && currTarget.CompareTag("Player"))
        {
            shotCounter++;
            foreach (var position in shootPositions)
            {
                Instantiate(bulletPref, position.position + transform.forward * 2, position.rotation);
            }

            shootsound.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            HP -= other.gameObject.GetComponent<Bullet>().Damage();
        }
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemy_2") || other.gameObject.CompareTag("PlayerBase"))
        {
            HP = 0;
        }
        else
        {
            HP--;
        }

        if (HP <= 0)
        {
            OnExplosion?.Invoke();
            GameObject g = Instantiate(explosionPref, transform.position, Quaternion.identity);
            Destroy(g, 2);
            Destroy(gameObject);
        }
    }
}
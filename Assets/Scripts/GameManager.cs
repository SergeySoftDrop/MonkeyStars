using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameConfig gameConfig;

    public Transform playerBase;

    public GameObject enemy_1Prefab;
    public GameObject enemy_2Prefab;
    public GameObject meteoritePrefab;

    private List<GameObject> enemies = new List<GameObject>();

    private bool enemySpawnReloading = false;

    private float enemyReloadSpawnTimer = 0f;
    private int enemySpawnCount = 0;

    void Start()
    {
        GenerateEnemies();
        GenerateMeteorites();
    }

    void Update()
    {
        GenerateEnemies();
    }

    void GenerateEnemies()
    {
        if (enemySpawnReloading)
        {
            enemyReloadSpawnTimer += Time.deltaTime;
            if (enemyReloadSpawnTimer >= gameConfig.Enemy_1SpawnRate)
            {
                enemyReloadSpawnTimer = 0;
                enemySpawnReloading = false;
            }
            else
            {
                return;
            }
        }

        int spawnCount = 0;

        if(enemySpawnCount < gameConfig.Enemy_1Count)
        {
            if (enemySpawnCount == 0)
            {
                spawnCount = Random.Range(gameConfig.Enemy_1SpawnCountMin, gameConfig.Enemy_1SpawnCountMax);
            }
            else if (gameConfig.Enemy_1Count - enemySpawnCount <= gameConfig.Enemy_1SpawnCountMin)
            {
                spawnCount = gameConfig.Enemy_1Count - enemySpawnCount;
            }
            else
            {
                spawnCount = Random.Range(gameConfig.Enemy_1SpawnCountMin, gameConfig.Enemy_1SpawnCountMax);
            }
        }

        for(int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = playerBase.position + Random.insideUnitSphere * Random.Range(gameConfig.Enemy_1GenerateDistanceMin, gameConfig.Enemy_11GenerateDistanceMax);
            GameObject enemy = Instantiate(enemy_1Prefab, randomPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().target = playerBase.transform;
            enemies.Add(enemy);
            enemySpawnCount++;
        }

        enemySpawnReloading = true;
    }

    void GenerateMeteorites()
    {
        for(int i = 0; i < gameConfig.MeteorCount; i++)
        {
            Vector3 randomPos = Random.onUnitSphere * Random.Range(gameConfig.MeteorGenerateDistanceMin, gameConfig.MeteorGenerateDistanceMax);
            Instantiate(meteoritePrefab, randomPos, Quaternion.identity);
        }
    }

    void UpdateStats()
    {
        
    }
}
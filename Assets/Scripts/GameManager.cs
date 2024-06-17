using Assets.Scripts.Conf.Scripts;
using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameConfig gameConfig;

    public Transform playerBase;

    public GameObject enemy_1Prefab;
    public GameObject enemy_2Prefab;
    public GameObject meteoritePrefab;

    public InterfaceController interfaceController;
    public BasePlayer basePlayer;
    public Player player;

    private List<GameObject> enemies_1 = new List<GameObject>();
    private List<GameObject> enemies_2 = new List<GameObject>();

    private bool enemy_1SpawnReloading = false;
    private bool enemy_2SpawnReloading = false;

    private float enemy_1ReloadSpawnTimer = 0f;
    private float enemy_2ReloadSpawnTimer = 0f;

    private int enemy_1SpawnCount = 0;
    private int enemy_2SpawnCount = 0;

    private int destroyEnemyCount = 0;

    private int aliveMeteoritesCount = 0;

    void Start()
    {
        Time.timeScale = 1;

        basePlayer.onDamage += BaseDamage;
        basePlayer.OnDestroy += Lose;
        player.onDamage += PlayerDamaged;
        player.onDestroy += Lose;
        GenerateInitialEnemiesAndMeteorites();
    }

    void Update()
    {
        HandleEnemySpawnTimers();
    }

    private void GenerateInitialEnemiesAndMeteorites()
    {
        GenerateMeteorites();
        GenerateEnemies_1();
        GenerateEnemies_2();
    }

    private void HandleEnemySpawnTimers()
    {
        if (!enemy_1SpawnReloading)
        {
            GenerateEnemies_1();
        }
        else
        {
            enemy_1ReloadSpawnTimer += Time.deltaTime;
            if (enemy_1ReloadSpawnTimer >= gameConfig.Enemy_1SpawnRate)
            {
                enemy_1ReloadSpawnTimer = 0;
                enemy_1SpawnReloading = false;
            }
        }

        if (!enemy_2SpawnReloading)
        {
            GenerateEnemies_2();
        }
        else
        {
            enemy_2ReloadSpawnTimer += Time.deltaTime;
            if (enemy_2ReloadSpawnTimer >= gameConfig.Enemy_2SpawnRate)
            {
                enemy_2ReloadSpawnTimer = 0;
                enemy_2SpawnReloading = false;
            }
        }
    }

    private void GenerateEnemies_1()
    {
        int spawnCount = CalculateSpawnCount(enemy_1SpawnCount, gameConfig.Enemy_1Count, gameConfig.Enemy_1SpawnCountMin, gameConfig.Enemy_1SpawnCountMax);
        SpawnEnemies(enemy_1Prefab, spawnCount, gameConfig.Enemy_1GenerateDistanceMin, gameConfig.Enemy_1GenerateDistanceMax, enemies_1, ref enemy_1SpawnCount);
        enemy_1SpawnReloading = true;
    }

    private void GenerateEnemies_2()
    {
        int spawnCount = CalculateSpawnCount(enemy_2SpawnCount, gameConfig.Enemy_2Count, gameConfig.Enemy_2SpawnCountMin, gameConfig.Enemy_2SpawnCountMax);
        SpawnEnemies(enemy_2Prefab, spawnCount, gameConfig.Enemy_2GenerateDistanceMin, gameConfig.Enemy_2GenerateDistanceMax, enemies_2, ref enemy_2SpawnCount);
        enemy_2SpawnReloading = true;
    }

    private int CalculateSpawnCount(int currentCount, int maxCount, int minSpawn, int maxSpawn)
    {
        if (currentCount >= maxCount)
        {
            return 0;
        }

        if (currentCount == 0)
        {
            return Random.Range(minSpawn, maxSpawn);
        }

        int remainingCount = maxCount - currentCount;
        if (remainingCount <= minSpawn)
        {
            return remainingCount;
        }

        return Random.Range(minSpawn, maxSpawn);
    }

    private void SpawnEnemies(GameObject enemyPrefab, int spawnCount, float minDistance, float maxDistance, List<GameObject> enemyList, ref int currentSpawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition(minDistance, maxDistance);
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

            var enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.target = playerBase.transform;
                enemyScript.OnExplosion += IncrementDestroyEnemyCount;
            }
            else
            {
                var enemy2Script = enemy.GetComponent<Enemy_2>();
                if (enemy2Script != null)
                {
                    enemy2Script.playerBase = player.basePlayer;
                    enemy2Script.player = player.gameObject;
                    enemy2Script.OnExplosion += IncrementDestroyEnemyCount;
                }
            }

            enemyList.Add(enemy);
            currentSpawnCount++;
        }
    }

    private Vector3 GetRandomSpawnPosition(float minDistance, float maxDistance)
    {
        return playerBase.position + Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
    }

    private void GenerateMeteorites()
    {
        int meteoritesToSpawn = (int)gameConfig.MeteorCount - aliveMeteoritesCount;
        for (int i = 0; i < meteoritesToSpawn; i++)
        {
            Vector3 randomPos = Random.onUnitSphere * Random.Range(gameConfig.MeteorGenerateDistanceMin, gameConfig.MeteorGenerateDistanceMax);
            GameObject meteorite = Instantiate(meteoritePrefab, randomPos, Quaternion.identity);
            meteorite.GetComponent<Meteorite>().OnDestroy += DecrementAliveMeteoritesCount;
            aliveMeteoritesCount++;
        }
    }

    private void DecrementAliveMeteoritesCount()
    {
        aliveMeteoritesCount--;
    }

    private void IncrementDestroyEnemyCount()
    {
        destroyEnemyCount++;
        interfaceController.UpdateEnemyCount(destroyEnemyCount);
        if (destroyEnemyCount >= gameConfig.Enemy_1Count + gameConfig.Enemy_2Count) interfaceController.Win();
    }

    private void BaseDamage()
    {
        interfaceController.UpdateBaseHPCount(basePlayer.health);
        Damage();
    }

    private void PlayerDamaged()
    {
        interfaceController.UpdatePlayerHPCount(player.HP);
        Damage();
    }

    private void Damage()
    {
        interfaceController.Damage();
    }

    private void Lose()
    {
        interfaceController.Lose();
    }
}
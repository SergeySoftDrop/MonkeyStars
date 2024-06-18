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
        GenerateInitialEnemiesAndMeteorites();
    }

    private void GenerateInitialEnemiesAndMeteorites()
    {
        GenerateMeteorites();
        HandleEnemySpawnTimers();
    }

    private void HandleEnemySpawnTimers()
    {
        if (enemy_1SpawnCount + enemy_2SpawnCount >= gameConfig.Enemy_1Count + gameConfig.Enemy_2Count) return;

        if(!enemy_1SpawnReloading)
        {
            GenerateEnemies_1();
            enemy_1SpawnReloading = true;
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
            enemy_2SpawnReloading = true;
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
        int spawnCount = 0;

        if(enemy_1SpawnCount >= gameConfig.Enemy_1Count) spawnCount = 0;
        if(enemy_1SpawnCount == 0) spawnCount = Random.Range(gameConfig.Enemy_1SpawnCountMin, gameConfig.Enemy_1SpawnCountMax);
        if(enemy_1SpawnCount > 0)
        {
            int remaining = gameConfig.Enemy_1Count - enemy_1SpawnCount;
            if(remaining > gameConfig.Enemy_1SpawnCountMin) spawnCount = Random.Range(gameConfig.Enemy_1SpawnCountMin, remaining);
            else spawnCount = remaining;
        }

        for(int i = 0; i < spawnCount; i++)
        {
            var randPos = GetRandomSpawnPosition(gameConfig.Enemy_2GenerateDistanceMax, gameConfig.Enemy_2GenerateDistanceMin);

            GameObject enemy = Instantiate(enemy_1Prefab, randPos, Quaternion.identity);

            Enemy script = enemy.GetComponent<Enemy>();
            script.target = playerBase;
            script.OnExplosion += IncrementDestroyEnemyCount;

            enemy_1SpawnCount++;
        }
    }

    private void GenerateEnemies_2()
    {
        int spawnCount = 0;

        if(enemy_2SpawnCount >= gameConfig.Enemy_2Count) spawnCount = 0;
        if(enemy_2SpawnCount == 0) spawnCount = Random.Range(gameConfig.Enemy_2SpawnCountMin, gameConfig.Enemy_2SpawnCountMax);
        if(enemy_2SpawnCount > 0)
        {
            int remaining = gameConfig.Enemy_2Count - enemy_2SpawnCount;
            if(remaining > gameConfig.Enemy_2SpawnCountMin) spawnCount = Random.Range(gameConfig.Enemy_2SpawnCountMin, remaining);
            else spawnCount = remaining;
        }

        for(int i = 0; i < spawnCount; i++)
        {
            var randPos = GetRandomSpawnPosition(gameConfig.Enemy_1GenerateDistanceMax, gameConfig.Enemy_1GenerateDistanceMin);

            GameObject enemy = Instantiate(enemy_2Prefab, randPos, Quaternion.identity);

            Enemy_2 script = enemy.GetComponent<Enemy_2>();
            script.player = player.player;
            script.playerBase = player.basePlayer;
            script.OnExplosion += IncrementDestroyEnemyCount;

            enemy_2SpawnCount++;
        }
    }

    private Vector3 GetRandomSpawnPosition(float minDistance, float maxDistance)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);
        return playerBase.position + randomDirection * randomDistance;
    }

    private void GenerateMeteorites()
    {
        int meteoritesToSpawn = (int)gameConfig.MeteorCount - aliveMeteoritesCount;
        for (int i = 0; i < meteoritesToSpawn; i++)
        {
            Vector3 randomPos = GetRandomSpawnPosition(gameConfig.MeteorGenerateDistanceMin, gameConfig.MeteorGenerateDistanceMax);
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
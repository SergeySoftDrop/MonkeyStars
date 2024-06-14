using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text statsText;
    public Text endGameTitle;
    public GameObject playerBase;
    public Transform playerBasePosition;
    public GameObject playerShip;
    public GameObject enemyPrefab;
    public GameObject meteoritePrefab;
    public GameObject pauseMenu;
    public int playerLives = 3;
    public int baseHealth = 10;
    public int enemyCount = 5;
    public int meteoriteCount = 5;
    private List<GameObject> enemies = new List<GameObject>();
    private bool isPaused = false;
    private bool isGameOver = false;

    void Start()
    {
        GenerateEnemies();
        GenerateMeteorites();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (playerLives <= 0 || baseHealth <= 0)
        {
            EndGame(false);
        }
        else if (enemies.Count == 0)
        {
            EndGame(true);
        }

        UpdateStats();
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            enemies.Add(enemy);
        }
    }

    void GenerateMeteorites()
    {
        for (int i = 0; i < meteoriteCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(meteoritePrefab, randomPosition, Quaternion.identity);
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    void EndGame(bool won)
    {
        isGameOver = true;
        endGameTitle.text = won ? "You Win!" : "Game Over";
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void UpdateStats()
    {
        
    }
}
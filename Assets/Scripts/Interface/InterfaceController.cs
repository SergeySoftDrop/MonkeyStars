using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public GameConfig gameConfig;

    public TextMeshProUGUI EnemyCount;
    public TextMeshProUGUI BaseHP;
    public TextMeshProUGUI PlayerHP;

    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public GameObject loseMenu;

    public GameObject blurPanel;
    public MoveJoystick MoveJoystick;
    public RotateJoystick RotateJoystick;

    public enum GameEnding {Lose, Win}

    private bool isPaused = false;

    private void Start()
    {
        UpdateEnemyCount(gameConfig.EnemyCount);
        UpdatePlayerHPCount(gameConfig.PlayerHP);
        UpdateBaseHPCount(gameConfig.BaseHP);
    }

    public void CLoseAnyModal()
    {
        
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
        ToggleBlur();
    }

    public void GameEnd(GameEnding res)
    {
        switch (res)
        {
            case GameEnding.Lose:
                Lose();
                break;
            case GameEnding.Win:
                Win();
                break;
        }
    }

    public void UpdateEnemyCount(int count)
    {
        EnemyCount.text = $"{count} / {gameConfig.EnemyCount}";
    }

    public void UpdatePlayerHPCount(int count)
    {
        PlayerHP.text = $"{count} / {gameConfig.PlayerHP}";
    }

    public void UpdateBaseHPCount(int count)
    {
        BaseHP.text = $"{count} / {gameConfig.BaseHP}";
    }

    private void Lose()
    {
        loseMenu.SetActive(true);
    }

    private void Win()
    {
        victoryMenu.SetActive(true);
    }

    private void ToggleBlur()
    {
        blurPanel.SetActive(!blurPanel.activeSelf);
    }
}

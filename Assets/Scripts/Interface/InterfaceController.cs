using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

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

    public PostProcessVolume postProcessVolume;
    public PostProcessProfile defaultProfile;
    public PostProcessProfile damageProfile;

    private bool damagedProfile = false;
    private float profileTimer = 0f;

    public enum GameEnding {Lose, Win}

    private bool isPaused = false;

    private void Update()
    {
        if(damagedProfile)
        {
            profileTimer += Time.deltaTime;

            if(profileTimer >= gameConfig.DamageEffectTimeInSeconds)
            {
                postProcessVolume.profile = defaultProfile;
                damagedProfile = false;
                profileTimer = 0;
            }
        }
    }

    private void Start()
    {
        UpdateEnemyCount(0);
        UpdatePlayerHPCount(gameConfig.PlayerHP);
        UpdateBaseHPCount(gameConfig.BaseHP);
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
        EnemyCount.text = $"{count} / {gameConfig.Enemy_1Count + gameConfig.Enemy_2Count}";
    }

    public void UpdatePlayerHPCount(float count)
    {
        PlayerHP.text = $"{count} / {gameConfig.PlayerHP}";
    }

    public void UpdateBaseHPCount(int count)
    {
        BaseHP.text = $"{count} / {gameConfig.BaseHP}";
    }

    public void Damage()
    {
        if(damagedProfile)
        {
            profileTimer = 0;
            return;
        }

        damagedProfile = true;
        postProcessVolume.profile = damageProfile;
    }

    public void Lose()
    {
        Time.timeScale = 0;
        ToggleBlur();
        loseMenu.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        ToggleBlur();
        victoryMenu.SetActive(true);
    }

    private void ToggleBlur()
    {
        blurPanel.SetActive(!blurPanel.activeSelf);
    }

    public void GameExit()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
}

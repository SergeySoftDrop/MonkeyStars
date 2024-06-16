using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public void NewGameClick()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGameClick()
    {
        Application.Quit();
    }
}

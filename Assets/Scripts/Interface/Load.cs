using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
    }
}

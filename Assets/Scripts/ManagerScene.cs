using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{

    public static ManagerScene Instance { get; private set; }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       

    }

    

    public void PlayGame()
    {
        SceneManager.LoadScene("Juego");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void IngresarNombres()
    {
        SceneManager.LoadScene("IngresarNombres");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

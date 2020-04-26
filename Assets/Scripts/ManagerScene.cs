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

    public void PlayGameTTS()
    {
        SceneManager.LoadScene("JuegoTTS");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
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

    public void PlayGameAndry()
    {
        SceneManager.LoadScene("Juego Andry");
    }
    public void PlayGameMarvind()
    {
        SceneManager.LoadScene("Juego Marvind");
    }
    public void PlayGameNorch()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Juego Norch");
    }
    public void PlayGameTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Juego Tutorial");
    }
    public void HaciaElMenu()
    {
        SceneManager.LoadScene("HacieElMenu");
    }

    public void PausaActivada(bool estado)
    {
        if(estado) GameManager.Instance.GetComponent<AudioSource>().Pause();
        else GameManager.Instance.GetComponent<AudioSource>().UnPause();
        
        UI.Instance.PausaOn(estado);
        Time.timeScale = estado? 0:1;
    }
}

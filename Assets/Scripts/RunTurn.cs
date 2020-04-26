using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RunTurn : MonoBehaviour
{
    //  Envía la señal de que el tiempo para completar la frase terminó.
    public static event Action<bool> EnFraseTerminada;

    public bool startTurn;

    private string theme = "";

    public Slider timePrhaseSlider;
    public Slider timeRhymeSlider;

    public float timePerPrhase = 11f;
    public float timePerRhyme;
    public float initialTimePerRhyme;

    public GameObject[] themeButtons;

    public TextAsset dictionaryTextFile;
    private string theWholeFileAsOneLongString;
    private List<string> eachLine;

    public TextMeshPro phrase;

    public RhymeManager rhymeManager;

    [SerializeField] int rhymeCount = 0;             //contador de rima

    public string Theme { get => theme; set => theme = value; }
    public bool Pause { get; set; }

    private bool VFXUI = false;

    public void Inicializar()
    {
        //Debug.Log("Hello RunTurn");
        //Debug.Log(theme);
        Pause = false;

        startTurn = false;
        ThemeButtonsOn(true);

        if(BasesManager.Instancia != null)
        {
            timePrhaseSlider.maxValue = BasesManager.Instancia.TiempoFrase;
            timePrhaseSlider.value = BasesManager.Instancia.TiempoFrase;

            timePerRhyme = BasesManager.Instancia.TiempoFrase;

            timeRhymeSlider.maxValue = BasesManager.Instancia.TiempoFrase;
            timeRhymeSlider.value = BasesManager.Instancia.TiempoFrase;
        }
        else
        {
            timePrhaseSlider.maxValue = 4;
            timePrhaseSlider.value = 4;

            timePerRhyme = 4;

            timeRhymeSlider.maxValue = 4;
            timeRhymeSlider.value = 4;
        }

        //read txt with phrase
        theWholeFileAsOneLongString = dictionaryTextFile.text;

        eachLine = new List<string>();
        eachLine.AddRange(theWholeFileAsOneLongString.Split("\n"[0]));

        //phrase.text = "";



        UI.Instance.PalabrasOn(true);
    }

    public void MatarRunTurn()
    {
        startTurn = false;
        ThemeButtonsOn(false);
        enabled = false;

        UI.Instance.PalabrasOn(false);
    }

    void Update()
    {
        timePrhaseSlider.value = timePerPrhase;
        timeRhymeSlider.value = timePerRhyme;

        if (startTurn)
        {
            /*  desactivar btn gameobjects
            ThemeButtonsOn(false);
            */

            //empezar rima
            StartRhyme();
        }
    }

    private void StartRhyme()
    {
        //importa rima - ya lo hace el start

        if (!Pause)
        {
            timePerRhyme -= Time.deltaTime; //empieza a bajar tiempo de la rima
            if (timePerRhyme <= 2 && VFXUI == false)
            {
                UI.Instance.ParSys[0].Play();
                VFXUI = true;
            }

            if (timePerRhyme <= 0)
            {
                if(BasesManager.Instancia != null) timePerRhyme = BasesManager.Instancia.TiempoFrase;
                else timePerRhyme = 4;
                rhymeCount++;
                VFXUI = false;
                //  Emite la señal de que terminó la frase.
                try
                {
                    EnFraseTerminada(true);
                    //Debug.Log("Sin Pausa");
                }
                catch
                {
                    //Debug.Log("Este es el error.");
                }
            } 
        }

        /*
        if (rhymeCount == 0)
        {
            //pide inputs en el tiempo
            //oracion 1
            //clase global con (eventos) - Pendiente
            //temporalmente:
            //esperar drop item - hecho

            //Disparar las palabras de la derecha o los lados - hecho

            rhymeManager.OpenList(rhymeCount);
        }

        if (rhymeCount == 1)
        {
            rhymeManager.OpenList(rhymeCount);
        }

        if (rhymeCount == 2)
        {
            rhymeManager.OpenList(rhymeCount);
        }

        if (rhymeCount == 3)
        {
            rhymeManager.OpenList(rhymeCount);
        }

        if (rhymeCount > 3)
        {
            //turno player 2
            //Debug.Log("Player 2 turn");
            //evaluar puntaje
        }
        */
    }

    public void ThemeButtonsOn(bool state)
    {
        foreach (GameObject go in themeButtons)
        {
            go.SetActive(state);
        }
    }

    public void AsignarTemas(string[] temas)
    {
        for (int i = 0; i < themeButtons.Length; i++)
        {
            themeButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = temas[i];
        }
    }
}



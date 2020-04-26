using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{     
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
     
    public string theme;

    public bool themeSelected = false;

    public Slider timeSlider;

    public GameObject[] themeButtons;

    /*
    [SerializeField]
    public GameObject[] patrones;
    */

    [SerializeField]
    public Patron patron;

    public float timeToSelectTheme = 14f; //Consumido
    public float timePerRhyme = 11f; //RuTurn
    public float prhaseCount = 4;
    public float timePerPrhase;
    public float controlTimer = 0f;
    //public float timeBeforeSong = 10f;

    private float _timeToSelectTheme;
    private bool VFXRYME = false;
    private bool rimaEscogida = false;

    public bool startTimer;

    float myTime = 0;

    public RunTurn runTurn;

    public GameObject globalTimerGo;
    public TextMeshProUGUI globalTimerText;
    public bool globalTimer = false;


    private LectorDeTexto lector;

    public TextAsset[] Textos { get; set; }
    

    private void Awake()
    {
        
        patron = FindObjectOfType<Patron>();
    }

    public void Inicializar()
    {
        enabled = true;
        rimaEscogida = false;

        MatarLevelMng();

        runTurn.Inicializar();
        SelectThemeButton(false);
        runTurn.enabled = false;

        if(BasesManager.Instancia != null)audioSource.clip = BasesManager.Instancia.Instrumental;
        audioSource.time = 0;
        audioSource.Play();

        if(BasesManager.Instancia != null)_timeToSelectTheme = BasesManager.Instancia.TiempoRimas;
        else _timeToSelectTheme = 6.5f;

        //timePerPrhase = timePerRhyme / prhaseCount;
        myTime = _timeToSelectTheme;

        timeSlider.maxValue = _timeToSelectTheme;
        timeSlider.value = _timeToSelectTheme;

        lector = GetComponent<LectorDeTexto>();
        lector.Inicializar();

        UI.Instance.RimasOn(true);
        LeerTemas();

        startTimer = true;

        //DesactivarPatrones();

        //StartTimer();
        //SelectTheme();
    }

    public void MatarLevelMng()
    {
        rimaEscogida = false;

        audioSource.Stop();

        runTurn.Inicializar();
        runTurn.MatarRunTurn();

        //DesactivarPatrones();

        patron.MatarPatron();
    }

    private void Update()
    {
        timeSlider.value = _timeToSelectTheme;

        
        if (startTimer)
        {
            _timeToSelectTheme -= Time.deltaTime;
            if (timeSlider.value <= 3 && VFXRYME==false)
            {
                UI.Instance.ParSys[1].Play();
                VFXRYME = true;


                //print("PArticulasFuncionando");
            }
            
            if (_timeToSelectTheme <= 0)
            {

                //game over
                RunTurn();

                startTimer = false;
                UI.Instance.RimasOn(false);
                VFXRYME = false;

                if(!rimaEscogida) SelectThemeButton(UnityEngine.Random.Range(0, themeButtons.Length));
                //  Desactiva el aviso de improvisa.
                UI.Instance.ImprovisaOn(false);
            }
        }

        /*if (globalTimer)
        {
            int timerUi = (int)_timeToSelectTheme;
            globalTimerText.text = timerUi.ToString();
            if (timerUi <= 0)
            {
                //  Desactiva el temporizador.
                globalTimer = false;
                globalTimerGo.SetActive(false);

                //  Desactiva las rimas.
                
            }
        }*/
    }

    private void RunTurn()
    {
        runTurn.timeRhymeSlider.gameObject.SetActive(true);
        //runTurn.phrase.enabled = true;

        runTurn.enabled = true;

        //runTurn.ThemeButtonsOn(false);
        runTurn.startTurn = true;
        audioSource.time = 6.4f; 

        runTurn.Theme = theme;

        TextToSpeech.Instance.SetTTS();
        TextToSpeech.Instance.Speak();

        this.enabled = false;
    }

    public void BtnThemeA()
    {
        theme = "A";
        SelectThemeButton(0);
    }

    public void BtnThemeB()
    {
        theme = "B";
        SelectThemeButton(1);
    }

    public void BtnThemeC()
    {
        theme = "C";
        SelectThemeButton(2);
    }

    public void BtnThemeD()
    {
        theme = "D";
        SelectThemeButton(3);
    }

    private void EnableThemeButtons(bool state)
    {
        foreach (GameObject button in themeButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }

    /*private void ActiveGlobalTimer()
    {
        globalTimer = true;
        globalTimerGo.SetActive(true);
    }*/

    private void DesactivarPatrones()
    {
        /*
        foreach(GameObject patron in patrones)
        {
            patron.GetComponent<Patron>().Inicializar();
            patron.GetComponent<Patron>().MatarPatron();
            patron.SetActive(false);
        }
        */

        patron.MatarPatron();
        patron.gameObject.SetActive(false);
    }

    /// <summary>
    /// Seleciona uno de los botones de rima.
    /// </summary>
    /// <param name="button"></param>
    public void SelectThemeButton(int button)
    {
        rimaEscogida = true;

        UI.Instance.TableroOn(true);
        UI.Instance.ImprovisaOn(true);
        EnableThemeButtons(false);
        themeButtons[button].transform.GetChild(0).gameObject.SetActive(true);
        // ActiveGlobalTimer();

        /*
        patrones[button].SetActive(true);
        patrones[button].GetComponent<Patron>().Inicializar();
        */

        runTurn.Pause = false;
        patron.gameObject.SetActive(true);
        patron.TextoTxt = Textos[button];
        patron.Inicializar();
    }

    /// <summary>
    /// Habilita los botones de rima.
    /// </summary>
    /// <param name="state"></param>
    public void SelectThemeButton(bool state)
    {
        UI.Instance.TableroOn(false);
        EnableThemeButtons(true);
        for (int h = 0; h < themeButtons.Length; h++)
        {
            themeButtons[h].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void LeerTemas()
    {
        string[] temas = lector.LeerTemas(Textos);

        runTurn.AsignarTemas(temas);
    }
}

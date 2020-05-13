using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    #region Sigleton
    public static MenuController instance;

    public MenuController Instance
    {
        get
        {
            if(instance == null)
            {
                Instance = gameObject.AddComponent<MenuController>();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public void Awake()
    {
        if(primerUsoDelJuego) PlayerPrefs.SetInt("PrimeraVez", 0);

        instance = this;

        InicializarControl();
    }

    #endregion

    public bool primerUsoDelJuego = false;


    [SerializeField]
    Button boton;
    [SerializeField]
    GameObject animBoton;

    [SerializeField]
    GameObject indicaciones;

    public void InicializarControl()
    {
        if(PlayerPrefs.GetInt("PrimeraVez") == 0)
        {
            if(boton != null) BloquearBoton();
            if(animBoton != null) animBoton.SetActive(false);
        }
        else if(animBoton != null) animBoton.SetActive(true);

        if(indicaciones != null && PlayerPrefs.GetInt("PrimeraVez") == 1) 
        {
            InvertirIndicaciones();
            PlayerPrefs.SetInt("PrimeraVez", 2);
        }
    }

    public void BloquearBoton()
    {
        boton.interactable = false;
    }

    public void TerminarTutorial()
    {
        PlayerPrefs.SetInt("PrimeraVez", 1);
    }

    public void InvertirIndicaciones()
    {
        indicaciones.SetActive(!indicaciones.activeSelf);
    }
}

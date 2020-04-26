using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] indicaciones;
    [SerializeField, Range(0,1)] 
    private float velocidadTutorial = 0.8f;
    [SerializeField]
    private AudioSource elAudio;
    
    private int indicacionActual;

    void Awake()
    {
        indicacionActual = 0;

        ApagarIndicaciones();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Indicaciones"))
        {
            IndicacionActualActiva(true);
        }
    }

    void PasarIndicacion()
    {
        indicacionActual++;
    }

    public void IndicacionActualActiva(bool estado)
    {
        if(indicaciones[indicacionActual] != null)
        {
            indicaciones[indicacionActual].SetActive(estado);
            if(!estado) PasarIndicacion();
        }
        
        Time.timeScale = estado? 
            PausarJuego(true): PausarJuego(false);
    }

    void ApagarIndicaciones()
    {
        foreach(GameObject i in indicaciones)
        {
            i.SetActive(false);
        }
    }

    private float PausarJuego(bool estado)
    {
        if(estado) 
        {
            UI.Instance.BloqueoOn(true);
            if(indicacionActual <= indicaciones.Length - 3) elAudio.Pause();
        }
        else 
        {
            UI.Instance.BloqueoOn(false);
            elAudio.pitch = velocidadTutorial;
            elAudio.UnPause();
        }

        return estado? 
            0:(indicacionActual >= 2)? 
                velocidadTutorial : 1;
    }
}

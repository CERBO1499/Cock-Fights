using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBUTTONS : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] scratches;

    AudioSource efecto;

    private void Awake()
    {
        efecto = GetComponent<AudioSource>();
    }

    public void ActivarParticulasPalabra()
    {
        UI.Instance.ParSys[2].Play();
        ScratchAleatorio();
    }
    public void ActivarParticulasPalabra1()
    {
        UI.Instance.ParSys[3].Play();
        ScratchAleatorio();
    }
    public void ActivarParticulasPalabra2()
    {
        UI.Instance.ParSys[4].Play();
        ScratchAleatorio();
    }

    private void ScratchAleatorio()
    {
        if(efecto == null) efecto = GetComponent<AudioSource>();

        efecto.clip = scratches[UnityEngine.Random.Range(0, scratches.Length)];
        efecto.Play();
    }
}

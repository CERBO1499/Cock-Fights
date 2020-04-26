using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Intencion { agresivo, comico, ego }

[Serializable]
public struct Posibilidades
{
    private const int NUM_PALABRAS = 3;

    /*
    [SerializeField]
    public string agresiva;
    [SerializeField]
    public string ego;
    [SerializeField]
    public string comica;

    public string[] ComoArreglo()
    {
        arreglo = new string[NUM_PALABRAS];

        arreglo[0] = agresiva;
        arreglo[1] = ego;
        arreglo[2] = comica;

        return arreglo;
    }
    */
    [SerializeField]
    private string[] palabras;

    public string[] Palabras { get => palabras;
        set 
        {
            palabras = value;    
        } 
    }

    public static Intencion[] Intenciones()
    {
        Intencion[] intenciones = new Intencion[NUM_PALABRAS];

        intenciones[0] = Intencion.agresivo;
        intenciones[1] = Intencion.ego;
        intenciones[2] = Intencion.comico;

        return intenciones;
    } 
}

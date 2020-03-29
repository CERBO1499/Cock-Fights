using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TematicaGeneral
{
    [SerializeField]
    string tematica;

    [SerializeField]
    TextAsset[] patrones;

    public TextAsset[] PatronesAleatorios(int cantPatrones)
    {
        TextAsset[] patronesA = new TextAsset[cantPatrones];

        int r0 = new System.Random().Next(0, patronesA.Length);
        int r1 = new System.Random().Next(0, patronesA.Length);
        int r2 = new System.Random().Next(0, patronesA.Length);
        int r3 = new System.Random().Next(0, patronesA.Length);

        patronesA[0] = patrones[0];
        patronesA[1] = patrones[1];
        patronesA[2] = patrones[2];
        patronesA[3] = patrones[3];

        /*
        for (int i = 0; i < patronesA.Length; i++)
        {
            Debug.Log(patronesA[i]);
        }
        */

        return patronesA;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TematicaGeneral
{
    [SerializeField]
    private string concepto;

    [SerializeField]
    TextAsset[] patrones;

    public string Concepto { get => concepto; }

    public TextAsset[] PatronesAleatorios(int cantPatrones)
    {
        TextAsset[] patronesA = new TextAsset[cantPatrones];
        List<TextAsset> _patrones = new List<TextAsset>();
        int r = 0;

        for (int j = 0; j < patrones.Length; j++)
        {
            _patrones.Add(patrones[j]);
        }

        for (int i = 0; i < patronesA.Length; i++)
        {
            r = UnityEngine.Random.Range(0, _patrones.Count);
            patronesA[i] = _patrones[r];
            _patrones.RemoveAt(r);
        }

        /*
        for (int i = 0; i < patronesA.Length; i++)
        {
            Debug.Log(patronesA[i]);
        }
        */

        return patronesA;
    }
}

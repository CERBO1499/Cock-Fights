using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TempoBase
{
    [SerializeField]
    public AudioClip instrumental;
    [SerializeField]
    public string nombre;
    [SerializeField]
    public float tiempoRimas;
    [SerializeField]
    public float tiempoFrase;
}

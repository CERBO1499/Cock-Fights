using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TempoBase
{
    private const string RUTA_BASES = "Bases/";

    public string RutaInstrumental{ get => RUTA_BASES + nombre; }
    public string nombre;
    public int precio;
    public bool comprada;

    public TempoBase(string n, int p, bool c)
    {
        nombre = n;
        precio = p;
        comprada = c;
    }
}


[Serializable]
public class ListaTempoBase
{
    public List<TempoBase> basesGuardadas;

    public ListaTempoBase(List<TempoBase> bases)
    {
        basesGuardadas = bases;
    }
}
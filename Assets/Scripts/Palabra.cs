using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Palabra : MonoBehaviour
{
    [SerializeField]
    protected string palabra;

    /*
    private void Awake(string palabra)
    {
        this.palabra = palabra;
    }
    */
    public abstract void Inicializar(string palabra, Intencion intencion);
    public abstract void Escoger();
    
}

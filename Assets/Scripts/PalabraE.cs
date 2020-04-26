using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PalabraE : Palabra
{
    //  Envía como mensaje la palabra y su intención.
    public static event Action<string, Intencion> EnPalabraEscogida;

    private bool escogida;
    private const int puntaje = 100;
    Intencion intencion;


   

   

    //  Define la palabra escogible.
    public override void Inicializar(string palabra, Intencion intencion)
    {
        //  Establece la palabra y la intención.
        this.palabra = palabra;
        this.intencion = intencion;
        //  Muestra la palabra.
        UI.Instance.MostrarPalabra(gameObject, palabra);

        //  La palabra no ha sido escogida.
        escogida = false;
    }

    //  Emite la palabra y su intención.
    public override void Escoger()
    {
        UI.Instance.PalabrasOn(false);

        //  Da por escogida la palabra.

        EnPalabraEscogida(palabra, intencion);
        try
        {
            if (!escogida)
            {
                //EnPalabraEscogida(palabra, intencion);
                
                escogida = true;
                
            }
        }
        catch
        {
            //  Me atrapaste wexd
        }
    }
}

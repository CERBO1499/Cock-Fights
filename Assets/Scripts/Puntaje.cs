using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CelebrationType { Breve, Media, Fuerte }

public class Puntaje : MonoBehaviour
{

    public float tiempoCelebracion;
    public static Intencion intFrase;

    private float[] puntajes;

    private float puntajeAnterior;

    public float Puntos { get; set; }
    public int Ultima { get; set; }
    public void Awake()
    {
        Patron.EnUltima += EsLaUltima;

        puntajes = new float[2];

        puntajeAnterior = 0f;
        Ultima = 0;

        RunTurn.EnFraseTerminada += Celebrar;
    }

    public void SumarPuntaje(Intencion intPalabra, int usuario)
    {
        Puntos = Puntuar(intFrase, intPalabra);
        
        puntajes[usuario] += Puntos;
    }

    public void Celebrar(bool i)
    {
        bool encadenado = false;

        if (Puntos == 1)
        {
            StartCoroutine(Celebracion(CelebrationType.Fuerte));
        }
        else if (Puntos == 0.5f)
        {
            if (puntajeAnterior == Puntos)
            {
                StartCoroutine(Celebracion(CelebrationType.Fuerte));
                encadenado = true;
            }
            else
            {
                StartCoroutine(Celebracion(CelebrationType.Media));
            }
        }
        else if (Puntos == 0.25f)
        {
            if (puntajeAnterior == Puntos)
            {
                encadenado = true;
            }
            else
            {
                StartCoroutine(Celebracion(CelebrationType.Breve));
            }
        }

        if (!encadenado) puntajeAnterior = Puntos;
        else puntajeAnterior = 0;
    }

    public float Puntuar(Intencion frase, Intencion palabra)
    {
        switch (frase)
        {
            case Intencion.agresivo:
                if (palabra == Intencion.agresivo) return 1f;
                else if (palabra == Intencion.comico) return 0.25f;
                else if (palabra == Intencion.ego) return 0.5f;
                else break;

            case Intencion.comico:
                if (palabra == Intencion.agresivo) return 0.5f;
                else if (palabra == Intencion.comico) return 1f;
                else if (palabra == Intencion.ego) return 0.25f;
                else break;

            case Intencion.ego:
                if (palabra == Intencion.agresivo) return 0.5f;
                else if (palabra == Intencion.comico) return 0.25f;
                else if (palabra == Intencion.ego) return 1f;
                else break;
        }

        return 0;
    }

    public static void CambiarIntFrase(Intencion intencion)
    {
        intFrase = intencion;
    }

    IEnumerator Celebracion(CelebrationType tipoCelebracion)
    {
        UI.Instance.CelebraOn(tipoCelebracion);
        yield return new WaitForSeconds(tiempoCelebracion);
        if (Ultima == 1) Ultima += 1;
        else if (Ultima >= 2)
        {
            UI.Instance.CelebraOn(false, 0);
            EsLaUltima(0);
        }
        else UI.Instance.CelebraOn(false);
    }

    public void VerificarGanador(string usuario1, string usuario2, Color color1, Color color2)
    {
        bool player1 = false;

        if (Usuario1 > Usuario2) player1 = true;
        else if (Usuario2 > Usuario1) player1 = false;
        else player1 = true;

        string usuario = player1 ? usuario1 : usuario2;
        Color color = player1 ? color1 : color2;
        float puntaje = player1 ? Usuario1 : Usuario2;

        UI.Instance.GanadorOn(usuario, color, puntaje);

        player1 = !player1;

        usuario = player1 ? usuario1 : usuario2;
        color = player1 ? color1 : color2;
        puntaje = player1 ? Usuario1 : Usuario2;

        UI.Instance.PerdedorOn(usuario, color, puntaje);
    }

    public void EsLaUltima(int val)
    {
        Ultima = val;
    }

    public float Usuario1 { get => puntajes[0] * 100; }
    public float Usuario2 { get => puntajes[1] * 100; }
}

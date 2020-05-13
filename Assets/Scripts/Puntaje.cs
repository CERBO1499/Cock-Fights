using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CelebrationType { Breve, Media, Fuerte, Descansa }

public class Puntaje : MonoBehaviour
{

    public float tiempoCelebracion;
    public static Intencion intFrase;

    private static float[] puntajes;

    private float puntajeAnterior;

    private bool palabraEscogida;

    public float Puntos { get; set; }
    public int Ultima { get; set; }
    public CelebrationType Celebra { get; set; }

    public void Awake()
    {
        puntajes = new float[2];

        for(int i = 0; i < puntajes.Length; i++)
        {
            puntajes[i] = 0;
        }

        puntajeAnterior = 0f;
        Ultima = 0;

        palabraEscogida = false;
    }

    public void Inicializar()
    {
        puntajes = new float[2];

        for(int i = 0; i < puntajes.Length; i++)
        {
            puntajes[i] = 0;
        }

        puntajeAnterior = 0f;
        Ultima = 0;

        /*//  Anuncia la última frase.
            if(numFrase == frases.Length - 1)
            {
                EnUltima(1);
            }*/
    }

    public void SumarPuntaje(Intencion intPalabra, int usuario)
    {
        Puntos = Puntuar(intFrase, intPalabra);
        
        puntajes[usuario] += Puntos;
    }

    public void Celebrar(bool i)
    {
        if(palabraEscogida)
        {
            StartCoroutine(Celebracion(0));
        }
        palabraEscogida = false;
    }

    public void Celebrar(string i, Intencion a)
    {
        palabraEscogida = true;

        Puntos = Puntuar(intFrase, a);

        bool encadenado = false;
        Celebra = CelebrationType.Descansa;

        if (Puntos == 1)
        {
            Celebra = CelebrationType.Fuerte;
        }
        else if (Puntos == 0.5f)
        {
            if (puntajeAnterior == Puntos)
            {
                Celebra = CelebrationType.Fuerte;
                encadenado = true;
            }
            else
            {
                Celebra = CelebrationType.Media;
            }
        }
        else if (Puntos == 0.25f)
        {
            if (puntajeAnterior == Puntos)
            {
                Celebra = CelebrationType.Descansa;
                encadenado = true;
            }
            else
            {
                Celebra = CelebrationType.Breve;
            }
        }

        if (!encadenado) puntajeAnterior = Puntos;
        else puntajeAnterior = 0;

        //StartCoroutine(Celebracion());

        //Debug.Log("Celebración: " + Celebra);
        //Debug.Log("Encadenado: " + encadenado);
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

    IEnumerator Celebracion()
    {
        UI.Instance.HostOn(Celebra);
        yield return new WaitForSeconds(tiempoCelebracion);
        UI.Instance.HostOn(false);
    }

    IEnumerator Celebracion(int i)
    {
        UI.Instance.CelebraOn(Celebra);
        yield return new WaitForSeconds(tiempoCelebracion);
        UI.Instance.CelebraOn(false);
        
        if (Ultima >= 3)
        {
            UI.Instance.CelebraOn(false, 0);
            Ultima = 0;
        }

        Ultima += 1;

        //Debug.Log("Última: " + Ultima);
    }

    public void VerificarGanador(string usuario1, string usuario2, Color color1, Color color2)
    {
        if(Usuario1 == Usuario2) UI.Instance.EmpateOn(usuario1, usuario2, Usuario1);
        else
        {
            bool player1 = false;

            if (Usuario1 > Usuario2) player1 = true;
            else if (Usuario1 < Usuario2) player1 = false;
        
            string usuario = player1 ? usuario1 : usuario2;
            Color color = player1 ? color1 : color2;
            float puntaje = player1 ? Usuario1 : Usuario2;

            UI.Instance.GanadorOn(usuario, color, puntaje);

            BasesManager.Instancia.PuntajeAMonedas(puntaje);

            player1 = !player1;

            usuario = player1 ? usuario1 : usuario2;
            color = player1 ? color1 : color2;
            puntaje = player1 ? Usuario1 : Usuario2;

            if(FindObjectOfType<Tutorial>() == null)
            {
                UI.Instance.PerdedorOn(usuario, color, puntaje);
            } 
        }
    }

    public void EsLaUltima(int val)
    {
        Ultima = val;
    }

    public float Usuario1 { get => puntajes[0] * 100; }
    public float Usuario2 { get => puntajes[1] * 100; }
}

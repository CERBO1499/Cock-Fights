using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LectorDeTexto))]
public class Patron : MonoBehaviour
{
    public static event Action<int> EnUltima;

    public float posInicial, posFinal, posSiguiente;

    //  Velocidad del desplazamiento.
    [SerializeField]
    private float velMov = 3;

    private string patron;
    private float tiempoLimite;
    private float tiempoRima;
    //  Frases del patrón.
    private Frase[] frases;
    //  Índice de la frase actual.
    private int numFrase;
    //  Posición final del desplazamiento, posición original.
    private Vector3 llegada;

    [SerializeField]
    private TextAsset textoTxt;

    LectorDeTexto lector;


    public bool probando = false;

    public TextAsset TextoTxt { get => textoTxt; set => textoTxt = value; }

    public void Awake()
    {
        frases = GetComponentsInChildren<Frase>();

        if (probando)
        {
            Inicializar();
        }
        else
        {
            textoTxt = null;
        }
    }
    

    public void Inicializar()
    {
        RunTurn.EnFraseTerminada += PasarFrase;

        lector = GetComponent<LectorDeTexto>();

        CambiarLlegada(posInicial);

        numFrase = 0;

        lector.Inicializar();

        TranscribirTexto();

        frases[numFrase].Inicializar();
    }

    public void MatarPatron()
    {
        RunTurn.EnFraseTerminada -= PasarFrase;

        for (int i = 0; i < frases.Length; i++)
        {
            frases[i].Inicializar();
            frases[i].MatarFrase();
        }
    }

    //  Activa la siguiente frase.
    public void PasarFrase(bool i)
    {
        //  Habilita los botones de las palabras.
        UI.Instance.PalabrasOn(true);

        //Debug.Log("Wenas");
        //  Deshabilita todos los espacios de la frase actual.
        frases[numFrase].DeshabilitarFrase();
        numFrase++;
        //  Verifica si hay frases accesibles.
        if (numFrase < frases.Length)
        {
            //  Anuncia la última frase.
            if(numFrase == frases.Length - 1)
            {
                EnUltima(1);
            }
            //  Desplaza el patrón hacia arriba.
            CambiarLlegada(posSiguiente, 0);
            //  Activa la siguiente frase.
            frases[numFrase].Inicializar();
        }
        //  Si no hay más frases accesibles:
        else
        {
            //  Deja de recibir señal del tiempo para frases.
            RunTurn.EnFraseTerminada -= PasarFrase;
            //  Devuelve el patrón a su posición original.
            CambiarLlegada(posFinal);
            //  Termina el patrón.
            GameManager.Instance.TerminarRonda();
            //  Apaga el tablero.
            UI.Instance.TableroOn(false);
        }
    }

    public void CambiarLlegada(float y)
    {
        llegada = new Vector3(0, y, 0);
    }

    public void CambiarLlegada(float y, int i)
    {
        llegada = transform.position + (Vector3.up * y);
    }

    private void Update()
    {
        //  Desplaza el patrón hasta la posición indicada.
        transform.position = Vector3.Lerp(
                transform.position,
                llegada,
                Time.deltaTime * velMov);
    }

    /// <summary>
    /// Extrae la información necesaria del texto y la ubica en las frases.
    /// </summary>
    private void TranscribirTexto()
    {
        string[] _frases = lector.SepararRenglones(textoTxt);
        string[] intenciones = lector.LeerIntencionFrases(textoTxt);
        string[][] palabras = lector.LeerPalabras(textoTxt);

        for (int i = 0; i < frases.Length; i++)
        {
            frases[i].Contenido = _frases[i];
            frases[i].PalabrasFrase = palabras[i];
            frases[i].IntencionFrase = intenciones[i];

            frases[i].UbicarEspacio(lector, i,lector.CaracteresXRenglon, lector.InicioEspacios[i]);
        }
    }
}

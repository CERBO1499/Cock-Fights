using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LectorDeTexto : MonoBehaviour
{
    /*
    //  SINGLETON

    public static LectorDeTexto instancia;

    public static LectorDeTexto Instancia
    {
        get
        {
            if (instancia == null) instancia = new LectorDeTexto();
            return instancia;
        }
    }

    private void Awake()
    {
        if (Instancia != null)
        {
            Destroy(Instancia);
        }

        instancia = this;
    }

    //  SINGLETON
    */


    //[SerializeField]
    public int[] rengActual, rengs, inicioEspacios;

    [SerializeField]
    private int caracteresXRenglon = 18;

    private string[] _frases;

    public int CaracteresXRenglon { get => caracteresXRenglon; }
    public int[] InicioEspacios { get => inicioEspacios; }

    public void Inicializar()
    {
        _frases = new string[4];
        rengActual = new int[_frases.Length];
        rengs = new int[_frases.Length];
        inicioEspacios = new int[_frases.Length];

        rengActual = InicializarArreglo(rengActual, 1);
        rengs = InicializarArreglo(rengs, 1);
        inicioEspacios = InicializarArreglo(inicioEspacios, 0);
    }

    public string[] SepararRenglones(TextAsset texto)
    {
        string[] t = texto.text.Split("\n"[0]);

        for (int w = 0; w < _frases.Length; w++)
        {
            _frases[w] = t[w];
        }


        for (int h = 0; h < _frases.Length; h++)
        {
            bool k = true;
            string fraseChars = "";

            for (int i = 0; i < _frases[h].Length; i++)
            {
                bool insertable = false;

                fraseChars += _frases[h][i];

                if (_frases[h][i].ToString() == " ")
                {
                    insertable = true;
                }

                
                if (_frases[h][i].ToString() == "_" && k)
                {
                    inicioEspacios[h] = fraseChars.Length - 1;

                    rengActual[h] = (inicioEspacios[h] - caracteresXRenglon >= caracteresXRenglon) ? 3 : (inicioEspacios[h] >= caracteresXRenglon) ? 2 : 1;

                    int tempInicio = inicioEspacios[h];
                    inicioEspacios[h] = (inicioEspacios[h] - caracteresXRenglon >= caracteresXRenglon) ? 
                        tempInicio - caracteresXRenglon * 2 : 
                        (inicioEspacios[h] >= caracteresXRenglon) ?
                        tempInicio - caracteresXRenglon : tempInicio;

                    k = false;        
                }
                

                if (fraseChars.Length >= caracteresXRenglon && insertable && rengs[h] == 1)
                {

                    _frases[h] = _frases[h].Insert(fraseChars.Length - 1, "\n");
                    rengs[h]++;

                    //Debug.Log("Llegué.");
                }

                if (fraseChars.Length - caracteresXRenglon >= caracteresXRenglon && insertable && rengs[h] == 2)
                {

                    _frases[h] = _frases[h].Insert(fraseChars.Length - 1, "\n");
                    rengs[h]++;

                    //Debug.Log("Llegué 2.");
                }

                //Debug.Log(_frases[ñ][i]);
            }

           /*
            Debug.Log(rengActual[h]);
            Debug.Log(rengs[h]);
            //Debug.Log(inicioEspacios[h]);
            */
            
        }
        
        return _frases;
    }

    public string[] LeerIntencionFrases(TextAsset texto)
    {
        string[] intenciones = new string[_frases.Length];

        string[] t = texto.text.Split("\n"[0]);

        for(int i = 0; i < intenciones.Length; i++)
        {
            intenciones[i] = t[(_frases.Length) + i]; 
        }

        return intenciones;
    }

    public string[][] LeerPalabras(TextAsset texto)
    {
        string[][] palabras = new string[_frases.Length][];

        string[] t = texto.text.Split("\n"[0]);

        for (int i = 0; i < palabras.Length; i++)
        {
            palabras[i] = t[((_frases.Length * 2)) + i].Split(","[0]);
        }

        return palabras;
    }

    public string[] LeerTemas(TextAsset[] texto)
    {
        string[][] t = new string[texto.Length][];
        string[] tema = new string[_frases.Length];

        for (int i = 0; i < tema.Length; i++)
        {
            t[i] = texto[i].text.Split("\n"[0]);
            tema[i] = t[i][_frases.Length * 3]; 
        }

        return tema;
    }

    private int[] InicializarArreglo(int[] arreglo, int valor)
    {
        for (int i = 0; i < arreglo.Length; i++)
        {
            arreglo[i] = valor;
        }

        return arreglo;
    }
}

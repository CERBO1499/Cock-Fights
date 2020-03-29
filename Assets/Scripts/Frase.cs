using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Frase : MonoBehaviour
{
    //  Objetos donde se muestran las palabras a escoger.
    private PalabraE[] botones;

    //  Recibe una estructura que define 1 palabra por cada intención.
    /*
    [SerializeField]
    Posibilidades[] palabras = new Posibilidades[1];
    */
    [SerializeField]
    Posibilidades palabras;

    //  Índice del espacio actual.
    private int numEspacio;
    public Intencion intencion;
    //  Objetos reemplazables por palabras.
    //private Espacio[] espacios;
    private Espacio espacio;

    [SerializeField]
    private float sumaUbicacion = 0.1f, minUbicacion = -0.3f, SumaIter = 0.002f, incSumaIter = 0.0005f; 

    //  Texto de la frase.
    private TextMeshPro contenido;

    //  Controlador del conjunto de frases.
    //private Patron patron;

    private void Awake()
    {
        contenido = GetComponent<TextMeshPro>();
        espacio = GetComponentInChildren<Espacio>();

        palabras = new Posibilidades();
        intencion = Intencion.agresivo;
    }

    public void Inicializar()
    {
        //patron = GetComponentInParent<Patron>();

        Puntaje.CambiarIntFrase(intencion);

        //espacios = GetComponentsInChildren<Espacio>();

        numEspacio = 0;
        //sumaUbicacion = 0;

        //espacios[numEspacio].Inicializar();
        //espacios[numEspacio].Habilitado(true);
        espacio.Inicializar();
        espacio.Habilitado(true);

        botones = FindObjectsOfType<PalabraE>();

        //  Arreglo PALABRAS
        //AsignarPalabras(botones, palabras[numEspacio].ComoArreglo(), Posibilidades.Intenciones());

        AsignarPalabras();
    }

    public void MatarFrase()
    {
        /*
        for(int i = 0; i < espacios.Length; i++)
        {
            espacios[i].Inicializar();
            espacios[i].MatarEspacio();
        }
        */

        espacio.Inicializar();
        espacio.MatarEspacio();
    }

    //  Deshabilita todos los espacios.
    public void DeshabilitarFrase()
    {
        /*
        foreach(Espacio espacio in espacios)
        {
            espacio.Inicializar();
            espacio.Habilitado(false);
        }
        */

        espacio.Inicializar();
        espacio.Habilitado(false);
    }

    //  Ubica las palabras pertinentes de cada espacio en los objetos escogibles.
    void AsignarPalabras()
    {
        Intencion[] ints = Posibilidades.Intenciones();

        /*
        Debug.Log("Cant. de botones: " + botones.Length);
        Debug.Log("Cant. de palabras: " + palabras.Palabras.Length);
        Debug.Log("Cant. de intenciones: " + ints.Length);
        */

        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].Inicializar(palabras.Palabras[i], ints[i]);
        }
    }

    public string Contenido 
    {
        set 
        { 
            if (contenido == null) contenido = GetComponent<TextMeshPro>(); 
            contenido.text = value; 
        } 
    }

    public void UbicarEspacio(LectorDeTexto datos, int indice, float caracXReng, float caracInicio)
    {
        float x = 0, y = 0;
        float caracterSize = 0.2f, trimPercent = 0.3f;

        x = /*(3 / caracXReng)*/ 0.6f + (caracInicio * (caracterSize - (trimPercent * caracterSize)));

        switch (datos.rengs[indice])
        {
            case 1:
                y = 0.0f;
                break;

            case 2:
                y = (datos.rengActual[indice] > 1) ? -0.4f : 0.4f;
                break;

            case 3:
                y = (datos.rengActual[indice] > 2) ? -0.45f : (datos.rengActual[indice] > 1) ? 0.0f : 0.6f;
                break;
        }

        //Debug.Log("Le X: " + x);
        espacio.UbicarEspacio(new Vector3((x) - 2.25f, y, 0));
    }
    
    public string[] PalabrasFrase { get => palabras.Palabras; 
        set
        {
            palabras.Palabras = value;
        } 
    }

    public string IntencionFrase
    {
        set
        {
            intencion = DescifrarIntencion(value.ToLower());

            //Debug.Log("Nueva intención: " + intencion);
        }
    }

    private Intencion DescifrarIntencion(string valor)
    {
        switch (valor[0])
        {
            case 'a':
                return Intencion.agresivo;
                
            case 'c':
                return Intencion.comico;
                
            case 'e':
                return Intencion.ego;

            default:
                return Intencion.agresivo;
        }
    }

    /*
    //  Habilita el siguiente espacio en cuanto rellene el actual.
    public void PasarEspacio()
    {
        //  Desactiva la frase actual.
        espacios[numEspacio].Habilitado(false);
        espacios[numEspacio].Habilitado(false);
        //  Verifica si hay espacios disponibles en la frase.
        numEspacio++;
        if (numEspacio < espacios.Length)
        {
            //  Si los hay, habilita los botones...
            UI.Instance.PalabrasOn(true);
            //  ... y habilita el siguiente espacio.
            espacios[numEspacio].Habilitado(true);
            AsignarPalabras(botones, palabras[numEspacio].ComoArreglo(), Posibilidades.Intenciones());
        }
    }
    */

    //  Arreglo
    /*
    //  Ubica las palabras pertinentes de cada espacio en los objetos escogibles.
    void AsignarPalabras(PalabraE[] botones, string[] palabras)
    {
        Intencion[] ints = Posibilidades.Intenciones();

        Debug.Log("Cant. de botones: " + botones.Length);
        Debug.Log("Cant. de palabras: " + palabras.Length);
        Debug.Log("Cant. de intenciones: " + ints.Length);

        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].Inicializar(palabras[i], ints[i]);
        }
    }
    */
}

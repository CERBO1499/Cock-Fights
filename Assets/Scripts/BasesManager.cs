using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class BasesManager : MonoBehaviour
{
    //  SINGLETON
    public static BasesManager Instancia { get; private set; }

    public string Usuario1 { get; set; }
    public string Usuario2 { get; set; }


    public void Awake()
    {

        if (Instancia == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instancia.gameObject);
        }

        Instancia = this;

        Inicializar();
    }

    //  SINGLETON

    [SerializeField]
    TextMeshProUGUI nombre;

    [SerializeField]
    TempoBase[] bases = new TempoBase[1];

    private AudioSource parlante;
    private int baseActual;

    public void Inicializar()
    {
        DontDestroyOnLoad(this);

        parlante = GetComponent<AudioSource>();

        BaseAleatoria();
    }

    public void BaseSiguiente()
    {
        baseActual++;
        if (baseActual >= bases.Length) baseActual = 0;

        CambiarBase();
    }

    public void BaseAnterior()
    {
        baseActual--;
        if (baseActual < 0) baseActual = bases.Length - 1;

        CambiarBase();
    }

    public void BaseAleatoria()
    {
        baseActual = Random.Range(0, bases.Length);

        CambiarBase();
    }

    public void CambiarBase()
    {
        parlante.Stop();
        parlante.clip = bases[baseActual].instrumental;
        if(nombre != null) nombre.text = bases[baseActual].nombre;
        parlante.time = bases[baseActual].tiempoRimas - 0.2f;
        parlante.Play();

        //Debug.Log(baseActual);
    }

    public void Detener()
    {
        Instancia.Detener(0);
    }

    public void Detener(int i)
    {
        parlante.Stop();
    }

    public void RecibirUsuarios()
    {
        Instancia.RecibirUsuarios(0);
    }

    public void RecibirUsuarios(int l)
    {
        TMP_InputField[] inputs = FindObjectsOfType<TMP_InputField>();

        Usuario1 = inputs[0].text;
        Usuario2 = inputs[1].text;
    }

    public float TiempoRimas { get => bases[baseActual].tiempoRimas; }

    public float TiempoFrase { get => bases[baseActual].tiempoFrase; }

    public AudioClip Instrumental { get => bases[baseActual].instrumental; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class BasesManager : GuardaBases
{
    private const float TIMEPO_PARA_ESCOGER_TEMA = 6.6f;
    public const string NOMBRE_DE_LISTA = "Lista de bases.json";

    //  SINGLETON
    public static BasesManager Instancia { get; private set; }

    public string Usuario1 { get; set; }
    public string Usuario2 { get; set; }
    public int Monedas { get; set; }
    public float TiempoParaEscogerTema{get => TIMEPO_PARA_ESCOGER_TEMA;}


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

        //PlayerPrefs.SetInt("Monedas",10000);
        Monedas = PlayerPrefs.GetInt("Monedas");

        Cargar(NOMBRE_DE_LISTA);

        Inicializar();
    }

    //  SINGLETON

    [SerializeField]
    TextMeshProUGUI nombre;
    TextMeshProUGUI monedas;

    private AudioSource parlante;

    public void Inicializar()
    {
        DontDestroyOnLoad(this);

        monedas = GameObject.FindGameObjectWithTag("Monedas").GetComponent<TextMeshProUGUI>();
        if(monedas != null) monedas.text = Monedas.ToString();
        
        parlante = GetComponent<AudioSource>();

        BaseAleatoria();        
    }
public override void CambiarBase()
    {
        parlante.Stop();
        if(nombre != null) 
        {
            nombre.text = bases[baseActual].nombre;

            if(bases[baseActual].comprada) parlante.clip = Resources.Load<AudioClip>(bases[baseActual].RutaInstrumental);
        }
        parlante.time =  TIMEPO_PARA_ESCOGER_TEMA - 0.7f;
        parlante.Play();

        //Debug.Log(baseActual);
    }
public void CambiarBase(TempoBase tmpBase)
    {
        parlante.Stop();
        parlante.clip = Resources.Load<AudioClip>(tmpBase.RutaInstrumental);
       // if(nombre != null) nombre.text = bases[baseActual].nombre;
        parlante.time = TIMEPO_PARA_ESCOGER_TEMA - 0.7f;
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

    public void PuntajeAMonedas(float puntaje)
    {
        Monedas += (int)(puntaje/100f);
        PlayerPrefs.SetInt("Monedas", Monedas);
        
        if(monedas != null) monedas.text = Monedas.ToString();
    }

    public void Comprar(int precio)
    {
        Monedas -= precio;
        PlayerPrefs.SetInt("Monedas", Monedas);
    }

    public void AddSong(TempoBase song)
    {
        bases.Add(song);
    }
    public AudioClip Instrumental { get => Resources.Load<AudioClip>(bases[baseActual].RutaInstrumental); }
}
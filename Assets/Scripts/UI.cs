using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //  SINGLETON
    public static UI instance;
    private void Awake()
    {
        instance = this;
        parSys = GetComponentsInChildren<ParticleSystem>();
        main = parSys[0].main;
        Inicializar();
        


    }
    public static UI Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new UI();
            }
            return instance;
        }
    }
    //  SINGLETON

    //  Recibe los botones y el fondo.
    [SerializeField]
    private GameObject tablero;
    //  Recibe el conjunto de los componenetes de la elección de rimas.
    [SerializeField]
    private GameObject rima;
    //  Recibe la imagen previa al gameplay.
    [SerializeField]
    private GameObject improvisa;
    //  Recibe la pantalla de bloqueo.
    [SerializeField]
    private GameObject bloqueo;
    //  Recibe la pantalla de continuar.
    [SerializeField]
    private GameObject continuar;
    //  Recibe la pantalla de usuario.
    [SerializeField]
    private GameObject usuario;
    //  Recibe la pantalla de celebración.
    [SerializeField]
    private GameObject[] celebras;
    private GameObject celeb;
    //  Recibe la pantalla de ganador
    [SerializeField]
    private GameObject ganador;
    //  Recibe la pantalla de ganador
    [SerializeField]
    private GameObject perdedor;
    //  Recibe la pantalla de ganador
    [SerializeField]
    private GameObject concepto;
    //  Recibe el texto de host
    [SerializeField]
    private GameObject host;
    //  Recibe la pantalla de pausa
    [SerializeField]
    private GameObject pausa;


    ParticleSystem[] parSys;
    ParticleSystem.MainModule main;

    //  Palabras interactuables.
    private PalabraE[] btnPalabras;


    public string Concepto 
    { 
        set 
        {
            concepto.GetComponentInChildren<TextMeshProUGUI>().text = value;
        } 
    }
    public ParticleSystem[] ParSys
    {
        get => parSys;

    }

    public void Inicializar()
    {
        btnPalabras = FindObjectsOfType<PalabraE>();
        celeb = celebras[0].GetComponentInParent<Celebra>().gameObject;

        ContinuarOn(false);
        PausaOn(false);
        BloqueoOn(false);
        TableroOn(false);
        RimasOn(false);
        UsuarioOn(false);
        CelebraOn(false);
        GanadorOn(false);
        HostOn(false);
    }

    //  Define si el tablero está activo o inactivo.
    public void TableroOn(bool state)
    {
        tablero.SetActive(state);
        CelebraOn(true, 0);
    }
    public void RimasOn(bool state)
    {
        rima.SetActive(state);
        BloqueoOn(false);
        ContinuarOn(false);
    }
    public void ImprovisaOn(bool state)
    {
        improvisa.SetActive(state);
    }
    public void BloqueoOn(bool state)
    {
        bloqueo.SetActive(state);
    }
    public void ContinuarOn(float puntaje)
    {
        continuar.SetActive(true);

        TextMeshProUGUI text = continuar.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Puntaje: " + puntaje.ToString();
    }
    public void ContinuarOn(bool state)
    {
        continuar.SetActive(false);
    }
    public void UsuarioOn(string usuario, Color color)
    {
        //  Bloquea la pantalla mientras muestra el usuario.
        BloqueoOn(true);

        this.usuario.SetActive(true);
        //  Muestra el nombre del usuario.
        TextMeshProUGUI[] text = this.usuario.GetComponentsInChildren<TextMeshProUGUI>();
        text[1].text = " " + usuario;

        //  Muestra el color del usuario.
        RawImage imagen = this.usuario.GetComponent<RawImage>();
        imagen.color = color;
    }

    public void UsuarioOn(bool state)
    {
        usuario.SetActive(false);

        //  Desbloquea la pantalla.
        BloqueoOn(false);
    }

    public void CelebraOn(CelebrationType celeb)
    {
        int tipo = 0;

        if(celeb != CelebrationType.Descansa)
        {
            switch (celeb)
            {
                case CelebrationType.Breve: tipo = 0;
                    break;
                case CelebrationType.Media: tipo = 1;
                    break;
                case CelebrationType.Fuerte: tipo = 2;
                    break;
            }

            CelebraOn(true, 0);
            celebras[tipo].SetActive(true);
            celebras[tipo].GetComponent<AudioSource>().Play();
        }
    }

    public void CelebraOn(bool state)
    {
        foreach(GameObject celebra in celebras) celebra.SetActive(false);
    }
    public void CelebraOn(bool state, int indice)
    {
        celeb.SetActive(state);
    }

    //  Reemplaza el texto del botón por la palabra correspondiente.
    public void MostrarPalabra(GameObject objeto, string palabra)
    {
        TextMeshProUGUI text = objeto.GetComponentInChildren<TextMeshProUGUI>();
        text.text = palabra;
    }

    public void PalabrasOn(bool state)
    {
        foreach (PalabraE palabra in btnPalabras)
        {
            GameObject boton = palabra.gameObject;
            boton.GetComponent<Button>().interactable = state;
        }
    }

    public void GanadorOn(string usuario, Color color, float puntaje)
    {
        //  Activa la pantalla del ganador.
        ganador.SetActive(true);

        //  Muestra el nombre del usuario.
        TextMeshProUGUI[] text = ganador.GetComponentsInChildren<TextMeshProUGUI>();
        text[1].text = "¡" + usuario + "!";
        text[2].text = puntaje.ToString() + " puntos";

        //  Muestra el color del usuario.
        RawImage imagen = ganador.GetComponent<RawImage>();
        imagen.color = color;
    }

    public void PerdedorOn(string usuario, Color color, float puntaje)
    {
        //  Muestra el nombre del usuario.
        TextMeshProUGUI[] text = perdedor.GetComponentsInChildren<TextMeshProUGUI>();
        text[1].text = usuario;
        text[2].text = puntaje.ToString() + " puntos";
    }

    public void PerdedorOn(bool state)
    {
        //  Muestra el nombre del usuario.
        perdedor.gameObject.SetActive(false);
    }

    public void GanadorOn(bool state)
    {
        //  Activa la pantalla del ganador.
        ganador.SetActive(false);
    }

    public void HostOn(CelebrationType celeb)
    {
        AudioSource audio = host.GetComponent<AudioSource>();
        TextMeshProUGUI text = host.GetComponentInChildren<TextMeshProUGUI>();
        string grito = "";

        switch (celeb)
        {
            case CelebrationType.Breve: 
                grito = "Ohhhh";
                break;

            case CelebrationType.Media:
                grito = "¡Díselo!";
                break;

            case CelebrationType.Fuerte:
                grito = "¡RUIDOOOO!";
                break;

            case CelebrationType.Descansa:
                grito = "";
                break;
        }

        //if(grito!="") audio.Play();

        text.enabled = true;
        text.text = grito;
    }

    public void HostOn(bool state)
    {
        TextMeshProUGUI text = host.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "";
        text.enabled = false;
    }

    public void PausaOn(bool state)
    {
        pausa.SetActive(state);
    }
}

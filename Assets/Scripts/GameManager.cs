using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //  SINGLETON
    public static GameManager Instance{get;set;}

   private void Awake()
   {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
        }

        ronda = 0;
        InicializarGameMng(true, ronda);
   }
    //  SINGLETON

    public bool tutorial = false;
    public string usuario1, usuario2;
    public Color color1, color2;
    public int rondasJuego = 4;

    [SerializeField]
    private AudioSource Walla;

    [SerializeField]
    private float tiempoBloqueo, tiempoUsuario;

    [SerializeField]
    private TematicaGeneral[] conceptos;

    public Patron patron;

    private LevelManager lvlManager;
    private Puntaje puntaje;
    private bool player1;
    private int ronda;


    public void InicializarGameMng(bool player1, int ronda)
    {
        //patron = FindObjectOfType<Patron>();
        PalabraE.EnPalabraEscogida -= Instance.Celebrar;
        PalabraE.EnPalabraEscogida += Instance.Celebrar;

        RunTurn.EnFraseTerminada -= Instance.Celebrar;
        RunTurn.EnFraseTerminada += Instance.Celebrar;

        PalabraE.EnPalabraEscogida -= Instance.Puntuar;
        PalabraE.EnPalabraEscogida += Instance.Puntuar;
        

        lvlManager = GetComponent<LevelManager>();
        puntaje = GetComponent<Puntaje>();

        if (BasesManager.Instancia == null) usuario1 = "Eminem";
        else usuario1 = BasesManager.Instancia.Usuario1;

        if (BasesManager.Instancia == null) usuario2 = "Dr.Dre";
        else usuario2 = BasesManager.Instancia.Usuario2;

        if (usuario1 == "") usuario1 = "Eminem";
        if (usuario2 == "") usuario2 = "Dr.Dre";

        if(tutorial) usuario1 = "Ganaste";

        int ranConcepto = UnityEngine.Random.Range(0, conceptos.Length);
        UI.Instance.Concepto = conceptos[ranConcepto].Concepto;
        lvlManager.Textos = conceptos[ranConcepto].PatronesAleatorios(4);
        lvlManager.Inicializar();

        this.player1 = player1;

        string usuario = player1 ? usuario1 : usuario2;
        Color color = player1 ? color1 : color2;

        if(!tutorial) StartCoroutine(UsuarioTemporal(usuario, color));

        //NuevoPatron();
    }

    public void NuevoPatron()
    {
        lvlManager.runTurn.Pause = false;
        patron.Inicializar();
    }

    public void TerminarRonda()
    {
        StartCoroutine(BloqueoTemporal());
        lvlManager.runTurn.Pause = true;

        UI.Instance.ContinuarOn(player1? puntaje.Usuario1 : puntaje.Usuario2);
    }
    public void Continuar()
    {
        //  Suma una ronda completada
        ronda++;

        if(ronda >= rondasJuego - 1)
        {
            puntaje.EsLaUltima(0);
        }
        //  Si no hay más rondas, muestr el ganador.
        if(ronda >= rondasJuego)
        {
            TerminarJuego();
        }
        else
        {
            lvlManager.enabled = true;
            //patron.MatarPatron();
            lvlManager.MatarLevelMng();
            InicializarGameMng(player1 = !player1, ronda);

            //Debug.Log("Ronda: " + ronda);
        }
    }

    public void EstablecerPatron(Patron patron)
    {
        this.patron = patron;
    }

    public void Puntuar(string palabra, Intencion intencion)
    {
        int usuario = player1 ? 0 : 1;
        puntaje.SumarPuntaje(intencion, usuario);

        Debug.Log("Puntaje 1: " + puntaje.Usuario1 + "\t| Puntaje 2: " + puntaje.Usuario2);
    }

    public void TerminarJuego()
    {
        PalabraE.EnPalabraEscogida -= Instance.Celebrar;
        RunTurn.EnFraseTerminada -= Instance.Celebrar;
        PalabraE.EnPalabraEscogida -= Instance.Puntuar;
        
        lvlManager.runTurn.Pause = true;
        puntaje.VerificarGanador(usuario1, usuario2, color1, color2);
    }
    public void Celebrar(bool i)
    {
        if (puntaje == null) puntaje = GameManager.Instance.gameObject.GetComponent<Puntaje>();

        puntaje.Celebrar(i);
    }

    public void Celebrar(string i, Intencion a)
    {
        if (puntaje == null) puntaje = GameManager.Instance.gameObject.GetComponent<Puntaje>();

        puntaje.Celebrar(i,a);
    }

    IEnumerator BloqueoTemporal()
    {
        UI.Instance.BloqueoOn(true);
        yield return new WaitForSeconds(tiempoBloqueo);
        UI.Instance.BloqueoOn(false);
    }
    IEnumerator UsuarioTemporal(string usuario, Color color)
    {
        UI.Instance.UsuarioOn(usuario, color);
        yield return new WaitForSeconds(tiempoUsuario);
        UI.Instance.UsuarioOn(false);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //  SINGLETON
    private static GameManager instance;

   private void Awake()
   {
       instance = this;

        ronda = 0;
        InicializarGameMng(true, ronda);
   }

   public static GameManager Instance
   {
       get
       {
           if (instance == null)
           {
               instance = new GameManager();
           }

           return instance;
       }

   }

    //  SINGLETON

    public string usuario1, usuario2;
    public Color color1, color2;
    public int rondasJuego = 4;


    [SerializeField]
    private float tiempoBloqueo, tiempoUsuario;

    [SerializeField]
    private TematicaGeneral[] conceptos;

    private Patron patron;
    private LevelManager lvlManager;
    private Puntaje puntaje;
    private bool player1;
    private int ronda;


    public void InicializarGameMng(bool player1, int ronda)
    {
        //patron = FindObjectOfType<Patron>();

        PalabraE.EnPalabraEscogida -= Puntuar;
        PalabraE.EnPalabraEscogida += Puntuar;

        lvlManager = GetComponent<LevelManager>();
        puntaje = GetComponent<Puntaje>();

        usuario1 = BasesManager.Instancia.Usuario1;
        usuario2 = BasesManager.Instancia.Usuario2;

        System.Random ran = new System.Random();
        int ranConcepto = ran.Next(0, conceptos.Length);
        UI.Instance.Concepto = conceptos[ranConcepto].Concepto;
        lvlManager.Textos = conceptos[ranConcepto].PatronesAleatorios(4);
        lvlManager.Inicializar();

        this.player1 = player1;

        string usuario = player1 ? usuario1 : usuario2;
        Color color = player1 ? color1 : color2;

        StartCoroutine(UsuarioTemporal(usuario, color));

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
        puntaje.EsLaUltima(0);

        //  Suma una ronda completada
        ronda++;
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

            Debug.Log("Ronda: " + ronda);
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

    void TerminarJuego()
    {
        lvlManager.runTurn.Pause = true;
        puntaje.VerificarGanador(usuario1, usuario2, color1, color2);
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
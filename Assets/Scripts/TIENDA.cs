using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class TIENDA : GuardaBases
{
    private const string NOMBRE_DE_LISTA = "DatosTienda.json";
    

    [SerializeField]
    TextMeshProUGUI nombre,precio,comprada;

    [SerializeField] private Button btnComprada;
    
    private void Buy()
    {
        TempoBase laBase = bases[baseActual];

        BasesManager.Instancia.Comprar(bases[baseActual].precio);
        laBase.comprada = true;        
        laBase.precio = 0;
        bases[baseActual] = laBase;
        ModUI();

        BasesManager.Instancia.AddSong(laBase);
        BasesManager.Instancia.Guardar(BasesManager.NOMBRE_DE_LISTA);

        Guardar(NOMBRE_DE_LISTA);

        print("Comprada");
    }
    public void Awake()
    {
        comprada.gameObject.SetActive(false);

        Cargar(NOMBRE_DE_LISTA);

        BasesManager.Instancia.IniciarMonedas();

        BaseAleatoria();
    }
    public  void CheckBuy()
    {
        if (BasesManager.Instancia.Monedas > 0)
        {
            if (BasesManager.Instancia.Monedas >= bases[baseActual].precio && bases[baseActual].comprada==false )
            {
                Buy();
            }
        }
       
    }
    public override void CambiarBase()
    {
        BasesManager.Instancia.CambiarBase(bases[baseActual]);
        nombre.text = bases[baseActual].nombre;
        
     ModUI();
        
    }

    void ModUI()
    {
        if (bases[baseActual].comprada == true)
        {
            comprada.gameObject.SetActive(true);
            precio.gameObject.SetActive(false);
            btnComprada.gameObject.SetActive(false);
            
        }
        else
        {
            btnComprada.gameObject.SetActive(true);
            comprada.gameObject.SetActive(false);
            precio.gameObject.SetActive(true);
            precio.text = bases[baseActual].precio.ToString();
        }
    }
}

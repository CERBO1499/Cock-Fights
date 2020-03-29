using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Espacio : MonoBehaviour
{
    public RectTransform rectTransform;

    private TextMeshPro text;
    private Frase frase;
    private SpriteRenderer resalto;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Inicializar()
    {
        text = GetComponent<TextMeshPro>();
        frase = GetComponentInParent<Frase>();
        resalto = GetComponentInChildren<SpriteRenderer>();
    }

    public void Habilitado(bool estado)
    {
        if (estado)
        {
            PalabraE.EnPalabraEscogida += LlenarEspacio;
            //resalto.enabled = true;
        }
        else
        {
            PalabraE.EnPalabraEscogida -= LlenarEspacio;
            //resalto.enabled = false;
        }
    }

    void LlenarEspacio(string palabra, Intencion intencion)
    {
        text.text = palabra.ToLower();
        //frase.PasarEspacio();
    }

    public void MatarEspacio()
    {
        Vector3 pos = rectTransform.position;

        PalabraE.EnPalabraEscogida -= LlenarEspacio;
        UbicarEspacio(Vector3.zero);
        text.text = "";
        Habilitado(false);
    }

    public void UbicarEspacio(Vector3 posicion)
    {
        rectTransform.localPosition = posicion;
    }
}

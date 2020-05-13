using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GuardaBases : MonoBehaviour
{
    protected const string RUTA_LISTA_DE_BASES = "/Resources/Bases/";
    protected List<TempoBase> bases = new List<TempoBase>();
    protected int baseActual;
    protected string rutaJson;
    protected string jsonString;

    public void BaseSiguiente()
    {
        baseActual++;
        if (baseActual >= bases.Count) baseActual = 0;

        CambiarBase();
    }

    public void BaseAnterior()
    {
        baseActual--;
        if (baseActual < 0) baseActual = bases.Count - 1;

        CambiarBase();
    }

    public void BaseAleatoria()
    {
        baseActual = Random.Range(0, bases.Count);

        CambiarBase();
    }

    public virtual void CambiarBase() {}

    public void Guardar(string nombreLista)
    {
        rutaJson = Application.dataPath + RUTA_LISTA_DE_BASES + nombreLista;
        ListaTempoBase _bases = new ListaTempoBase(bases);
        jsonString = JsonUtility.ToJson(_bases);
        File.WriteAllText(rutaJson, jsonString);
    }
    protected void Cargar(string nombreLista)
    {
        rutaJson = Application.dataPath + RUTA_LISTA_DE_BASES + nombreLista;
        jsonString = File.ReadAllText(rutaJson);
        ListaTempoBase _bases = JsonUtility.FromJson<ListaTempoBase>(jsonString);
        bases = _bases.basesGuardadas;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //Debug.Log("Dropped object was: " + eventData.pointerDrag); //WORK

            string word = "";
            word = eventData.pointerDrag.GetComponent<TextMeshPro>().text;
            //Debug.Log(word);      //FUNCIONA!

            RhymeManager rhymeManager = GameObject.FindObjectOfType<RhymeManager>();
            rhymeManager.AddWordToList(word, rhymeManager.ListToAdd);
        }
    }
}

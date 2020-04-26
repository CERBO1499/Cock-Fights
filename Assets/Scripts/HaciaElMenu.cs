using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaciaElMenu : MonoBehaviour
{
    ManagerScene escenasMng;

    public void Awake()
    {
        escenasMng = GetComponent<ManagerScene>();

        escenasMng.MainMenu();
    }
}

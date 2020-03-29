using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanadorOn : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("sounds/winner");
        audioSource.Play();
    }
}

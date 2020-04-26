using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioActivationGeneral : MonoBehaviour
{
   
   public static AudioActivationGeneral Instance { get; private set; }
   public ICommand actvieAudio;
    private AudioSource reproductor;
   [SerializeField] private AudioClip [] temas;
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      sound = GetComponent<AudioSource>();
   }

   private void Start()
   {
      actvieAudio=new ActivarAudioCommand(this,temas);
   }

   //[SerializeField] private AudioClip [] efecto;
   private AudioSource sound;

   public void Reproducir()
   {

      if (reproductor == null) reproductor = GetComponent<AudioSource>();
      reproductor.clip = temas[UnityEngine.Random.Range(0, temas.Length)];;
      reproductor.Play();
      
    /*  if(sound == null) sound = GetComponent<AudioSource>();
      sound.clip = efecto[0];
      sound.Play();*/
   }
   

   public void ReproducirFX1()
   {
      actvieAudio.Execute();
   }

  
   
   
   
   
}

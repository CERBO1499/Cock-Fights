using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAudioCommand : ICommand
{
   

    private AudioActivationGeneral _audioActivationGeneral;
    private AudioClip [] tema;

    public ActivarAudioCommand(AudioActivationGeneral aud, AudioClip [] tem)
    {
        _audioActivationGeneral = aud;
        tema = tem;
    }
    
    public void Execute()
    {
        _audioActivationGeneral.Reproducir();
    }

   


    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Crosstales.RTVoice;

public class TextToSpeech : Singleton<TextToSpeech>
{
    [SerializeField]
    string voiceName;

    [TextArea(0,10)]
    [SerializeField]
    string[] phrases;

    [TextArea(0, 10)]
    [SerializeField]
    string textToSpeech;

    public string[] Phrases { get => phrases; set => phrases = value; }
    public string VoiceName { get => voiceName; set => voiceName = value; }

    public void Speak()
    {
        //Speaker.Speak(textToSpeech, null, Speaker.VoiceForName(voiceName));

        //Speaker.Speak(textToSpeech, null, Speaker.VoiceForName(voiceName), true, 1, 1, "helloWorld", 1);


    }

    /// <summary>
    /// Vuelve el arreglo de frases un texto para que la voz se ejecute a buen tiempo.
    /// </summary>
    public void SetTTS()
    {
        if(phrases != null)
        {
            string tts = "";

            for (int i = 0; i < phrases.Length; i++)
            {
                string temp;
                temp = phrases[i].Replace("______", "\n");
                temp = temp.Insert(temp.IndexOf(".") + 1, "\n");
                tts += temp;
            }
            textToSpeech = tts;
        }
    }
}

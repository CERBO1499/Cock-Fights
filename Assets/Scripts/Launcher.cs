using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject word;
    [SerializeField] int rateWord;
    [SerializeField] float nextTimeToWord = 0f;
    public string launcherSide = "";

    private void Update()
    {
        //if (Time.time >= nextTimeToWord)
        //{
        //    nextTimeToWord = Time.time + rateWord;
        //    DropWord();
        //}
    }

    public void DropWord(List<string> otherWord)
    {
        StartCoroutine(DropWordCoroutine(otherWord));
    }

    private IEnumerator DropWordCoroutine(List<string> otherWord)
    {
        for (int i = 0; i < otherWord.Count; i++)
        {
            GameObject myWord = Instantiate(word, transform.position, transform.rotation, transform);
            myWord.GetComponent<LaunchedWord>().Side = launcherSide;
            myWord.GetComponent<LaunchedWord>().Text = otherWord[i];
            yield return new WaitForSeconds(rateWord);
        }
    }
}

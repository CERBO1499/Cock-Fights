using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RhymeManager : MonoBehaviour
{
    public List<string> stringWordsRhymeOne = new List<string>();
    public List<string> stringWordsRhymeTwo = new List<string>();
    public List<string> stringWordsRhymeThree = new List<string>();
    public List<string> stringWordsRhymeFour = new List<string>();

    public List<string> stringLaunchWordsLeft = new List<string>();
    public List<string> stringLaunchWordsRight = new List<string>();

    [SerializeField] int listToAdd = 0;

    List<string> wordToLaunch = new List<string>();

    [SerializeField] bool canShootWords = true;

    public GameObject launcherLeft, launcherRight;

    public GameObject[] wordButtons;

    public int ListToAdd { get => listToAdd; set => listToAdd = value; }
    public List<string> WordToLaunch { get => wordToLaunch; set => wordToLaunch = value; }

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(listToAdd);
        //Debug.Log(wordToLaunch.Count);

        if (listToAdd == 1 && canShootWords)
        {
            canShootWords = false;
            PrepareWordsToLauch(4);
        }
        if (listToAdd == 2 && canShootWords)
        {
            canShootWords = false;
            PrepareWordsToLauch(4);
        }
        if (listToAdd == 3 && canShootWords)
        {
            canShootWords = false;
            PrepareWordsToLauch(4);
        }
        if (listToAdd == 4 && canShootWords)
        {
            canShootWords = false;
            PrepareWordsToLauch(4);
        }
    }

    public void AddWordToList(string word, int list)
    {
        switch (list)
        {
            case 0:
                if (stringWordsRhymeOne.Count < 2)
                {
                    stringWordsRhymeOne.Add(word);
                }
                break;
            case 1:
                if (stringWordsRhymeTwo.Count < 2)
                {
                    stringWordsRhymeTwo.Add(word);
                }
                break;
            case 2:
                if (stringWordsRhymeThree.Count < 2)
                {
                    stringWordsRhymeThree.Add(word);
                }
                break;
            case 3:
                if (stringWordsRhymeFour.Count < 2)
                {
                    stringWordsRhymeFour.Add(word);
                }
                break;
        }
    }

    public void OpenList(int rhymeCount)
    {
        listToAdd = rhymeCount;
    }

    public void PrepareWordsToLauch(int times)
    {
        for (int i = 0; i < times; i++)
        {
            wordToLaunch.Add(stringLaunchWordsLeft[i]);
        }

        //launcherLeft.GetComponent<Launcher>().DropWord(wordToLaunch);

        SetWorsInButtons();
    }

    private void SetWorsInButtons()
    {
        for (int i = 0; i < wordButtons.Length; i++)
        {
            wordButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = WordToLaunch[i];
        }
    }
}

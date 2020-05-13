using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    int sceneIndex;

    [SerializeField]
    Slider slider;

    private void Start()
    {
        Invoke("LoadLevel", 1f);
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(1 - (operation.progress / .9f));

            slider.value = progress;

            yield return null;
        }
    }
}

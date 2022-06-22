using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        StartCoroutine(LoadSceneCo());
    }

    private IEnumerator LoadSceneCo()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}

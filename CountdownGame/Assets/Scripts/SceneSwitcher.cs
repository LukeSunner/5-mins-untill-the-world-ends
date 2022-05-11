using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public int scene;
    private int currentScene;

    private void Start()
    {
        currentScene = SceneManager.sceneCountInBuildSettings;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(scene);
        print("Scene Loaded");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(scene + 1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
}

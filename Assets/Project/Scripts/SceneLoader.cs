using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private Scene currentScene;

    protected SceneLoader() {}
    
    private void OnEnable()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void LoadNextLevel()
    {
        Debug.Log("Current Scene is " + currentScene.name);

        int newSceneIndex = currentScene.buildIndex + 1;
        
        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
        
        Debug.Log("Current Scene is " + currentScene.name);
    }
    
    public void LoadPreviousLevel()
    {
        Debug.Log("Current Scene is " + currentScene.name);

        int newSceneIndex = currentScene.buildIndex - 1;

        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadSceneAsync(currentScene.buildIndex - 1);
        }
        
        Debug.Log("Current Scene is " + currentScene.name);
    }
}

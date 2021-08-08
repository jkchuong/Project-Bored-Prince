using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : SingletonPersistent<SceneLoader>
{
    protected SceneLoader() {}

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        // TODO: Loading screen
        // Load loading screen canvas
        // Switch to UI camera
        // Load new level
        // Switch to level camera
        // Remove loading screen

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed +=
            operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    public void LoadUIScene()
    {
        if (!SceneManager.GetSceneByName(Scenes.UI.ToString()).isLoaded)
        {
            SceneManager.LoadSceneAsync(Scenes.UI.ToString(), LoadSceneMode.Additive);
        }
    }

    public void UnloadUIScene()
    {
        SceneManager.UnloadSceneAsync(Scenes.UI.ToString());
    }
}

public enum Scenes
{
    Prototype1,
    Prototype2,
    UI
}
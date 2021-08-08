using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : SingletonPersistent<SceneLoader>
{
    protected SceneLoader() {}

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Additive);
    }
    
    public void LoadScene(Scenes sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName.ToString()).isLoaded)
            return;
     
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        // Place loading screen here
        
        // Load new scene additively and set it as the active scene when loaded
        SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive).completed +=
            operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ToString()));
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
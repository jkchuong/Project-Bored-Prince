using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : SingletonPersistent<SceneLoader>
{
    [SerializeField] private GameObject loadingCamera;
    [SerializeField] private GameObject CircleWipe;

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;
    
    public void ReloadScene()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Move loading screen canvas
        transition.SetTrigger("Start");
        
        // Wait for loading screen
        yield return new WaitForSeconds(transitionTime);
        
        // Switch to loading camera
        loadingCamera.SetActive(true);

        // Unload old scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        // Load new scene
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        asyncOperation.completed += 
            operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        
        yield return asyncOperation;

        // Switch to level camera
        loadingCamera.SetActive(false);
        
        // Remove loading screen
        transition.SetTrigger("End");
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
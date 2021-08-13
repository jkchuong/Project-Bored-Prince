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
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }
    
    public IEnumerator LoadScene(string sceneName)
    {
        
        // TODO: Replace with LeanTween
        // Move loading screen canvas
        transition.SetTrigger("Start");
        
        // Wait for loading screen
        yield return new WaitForSeconds(0.5f);
        
        // Switch to loading camera
        loadingCamera.SetActive(true);

        // Unload old scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        Debug.Log("Unloading scene async");

        // Load new scene
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        Debug.Log("Loading Scene Async");

        asyncOperation.completed += 
            operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        Debug.Log("Waiting for loading to finish");

        // yield return asyncOperation;
        yield return new WaitForSeconds(1f);
        
        Debug.Log("Finished loading"); // Not logged in console
        
        // Switch to level camera
        loadingCamera.SetActive(false);
        
        Debug.Log("Switching loading camera off");
        
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
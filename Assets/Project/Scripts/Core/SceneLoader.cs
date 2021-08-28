using System.Collections;
using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonPersistent<SceneLoader>
{
    [SerializeField] private Camera loadingCamera;
    [SerializeField] private RectTransform circleWipe;

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // TODO: Replace with DOTween
        // Move loading screen canvas
        StartLoadingScreen();       

        // Wait for loading screen
        yield return new WaitForSeconds(transitionTime);
        
        // Switch to loading camera
        loadingCamera.enabled = true;

        // Unload old scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        // Load new scene
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        asyncOperation.completed += 
            operation => SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        
        yield return asyncOperation;

        // Switch to level camera
        loadingCamera.enabled = false;
        
        // Remove loading screen
        EndLoadingScreen();
    }

    private IEnumerator LoadSceneInstant(string sceneName)
    {
        StartLoadingScreen();
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        EndLoadingScreen();
    }

    public void LoadLevelInstant(string sceneName)
    {
        StartCoroutine(LoadSceneInstant(sceneName));
    }

    public void LoadLevelWithAnimation(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    public static void LoadUIScene()
    {
        if (!SceneManager.GetSceneByName(Scenes.UI.ToString()).isLoaded)
        {
            SceneManager.LoadSceneAsync(Scenes.UI.ToString(), LoadSceneMode.Additive);
        }
    }

    public static void UnloadUIScene()
    {
        SceneManager.UnloadSceneAsync(Scenes.UI.ToString());
    }

    public void StartLoadingScreen()
    {
        transition.SetTrigger("Start");
    }
    
    public void EndLoadingScreen()
    {
        transition.SetTrigger("End");
    }
}

public enum Scenes
{
    Prototype1,
    Prototype2,
    UI
}
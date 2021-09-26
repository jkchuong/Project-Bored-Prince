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

    private bool coroutineAllowed = true;

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        coroutineAllowed = false;
        
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
        
        coroutineAllowed = true;
    }

    private IEnumerator LoadSceneInstant(string sceneName, bool unloadUI = true)
    {
        coroutineAllowed = false;

        StartLoadingScreen();
        yield return new WaitForSeconds(transitionTime);

        if (unloadUI)
        {
            UnloadUIScene();
        }
        
        SceneManager.LoadScene(sceneName);
        EndLoadingScreen();
        
        coroutineAllowed = true;
    }

    public void LoadLevelInstant(string sceneName, bool unloadUI = false)
    {
        if (coroutineAllowed)
            StartCoroutine(LoadSceneInstant(sceneName, unloadUI));
    }
    
    public void LoadLevelInstant(string sceneName)
    {
        if (coroutineAllowed)
            StartCoroutine(LoadSceneInstant(sceneName));
    }

    public void LoadLevelWithAnimation(string sceneName)
    {
        if (coroutineAllowed)
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
        if (SceneManager.GetSceneByName(Scenes.UI.ToString()).isLoaded)
        {
            SceneManager.UnloadSceneAsync(Scenes.UI.ToString());
        }
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
    Menu,
    Map,
    Level1,
    Level2,
    Level3,
    UI
}
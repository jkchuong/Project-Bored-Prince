using System;
using System.Collections;
using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : SingletonPersistent<SceneLoader>
{
    [SerializeField] private Camera loadingCamera;
    [SerializeField] private RectTransform circleWipe;

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    private float circleWipeWidth;
    private float halfCameraWidth;

    private void Start()
    {
        circleWipeWidth = circleWipe.sizeDelta.x;
        halfCameraWidth = loadingCamera.aspect * loadingCamera.orthographicSize;
    }

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
        // TODO: Replace with DOTween
        // Move loading screen canvas
        transition.SetTrigger("Start");
        // circleWipe.DOMoveX(0f, 3f, false);
        
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
        transition.SetTrigger("End");
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
}

public enum Scenes
{
    Prototype1,
    Prototype2,
    UI
}
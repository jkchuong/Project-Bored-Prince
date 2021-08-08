using UnityEngine.SceneManagement;


public class SceneLoader : SingletonPersistent<SceneLoader>
{
    private Scene currentScene;

    protected SceneLoader() {}

    private void OnEnable()
    {
        SetSceneToActiveScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentScene.buildIndex);
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SetSceneToActiveScene();
    }

    public void LoadNextSceneInBuild()
    {
        int newSceneIndex = currentScene.buildIndex + 1;
    
        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadScene(currentScene.buildIndex + 1);
            SetSceneToActiveScene();
        }
    }

    public void LoadPreviousSceneInBuild()
    {
        int newSceneIndex = currentScene.buildIndex - 1;

        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadScene(currentScene.buildIndex - 1);
            SetSceneToActiveScene();
        }
    }

    private void SetSceneToActiveScene()
    {
        currentScene = SceneManager.GetActiveScene();
    }
}
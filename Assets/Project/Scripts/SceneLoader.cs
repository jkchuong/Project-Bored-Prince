using UnityEngine.SceneManagement;

public class SceneLoader : SingletonPersistent<SceneLoader>
{
    private Scene currentScene;

    protected SceneLoader() {}
    
    private void OnEnable()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextSceneInBuild()
    {
        int newSceneIndex = currentScene.buildIndex + 1;
        
        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
        }
    }
    
    public void LoadPreviousSceneInBuild()
    {
        int newSceneIndex = currentScene.buildIndex - 1;

        if (newSceneIndex < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadSceneAsync(currentScene.buildIndex - 1);
        }
    }
}

using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private Scenes sceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.levelsUnlocked++;
            SceneLoader.Instance.LoadLevelInstant(sceneName.ToString(), true);
        }
    }
}

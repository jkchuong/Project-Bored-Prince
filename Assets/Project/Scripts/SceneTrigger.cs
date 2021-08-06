using System;
using UnityEngine;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }
    }
}

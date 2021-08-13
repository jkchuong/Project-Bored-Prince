using System;
using UnityEngine;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private Scenes sceneName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SceneLoader.Instance.LoadScene(sceneName.ToString()));
        }
    }
}

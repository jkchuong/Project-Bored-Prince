using System;
using UnityEngine;

public class UILoader : MonoBehaviour
{
    [SerializeField] private bool shouldLoadUI;
    
    private void Start()
    {
        if (shouldLoadUI)
        {
            SceneLoader.Instance.LoadUIScene();
        }
        else
        {
            SceneLoader.Instance.UnloadUIScene();
        }
    }
}

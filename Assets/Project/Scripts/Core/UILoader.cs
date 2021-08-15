using UnityEngine;

namespace Project.Scripts.Core
{
    public class UILoader : MonoBehaviour
    {
        [SerializeField] private bool shouldLoadUI;
    
        private void Start()
        {
            if (shouldLoadUI)
            {
                SceneLoader.LoadUIScene();
            }
            else
            {
                SceneLoader.UnloadUIScene();
            }
        }
    }
}

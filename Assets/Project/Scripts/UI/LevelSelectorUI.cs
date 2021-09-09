using System;
using System.Collections.Generic;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class LevelSelectorUI : MonoBehaviour
    {
        [SerializeField] private List<Scenes> levels;

        private int arrayPosition;
        private CharBezierFollow charBezierFollow;
        
        private void Start()
        {
            charBezierFollow = FindObjectOfType<CharBezierFollow>();
            arrayPosition = levels.IndexOf(GameManager.Instance.currentScene);
            charBezierFollow.SetPosition(arrayPosition);
        }

        private void Update()
        {

            if (!charBezierFollow.coroutineAllowed) return;
            
            // Press A move left of array
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (arrayPosition - 1 >= 0)
                {
                    arrayPosition -= 1;
                    charBezierFollow.MoveOnRouteReverse(arrayPosition);
                }
            }
            
            // Press D move right of array
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (arrayPosition + 1 < levels.Count
                    && arrayPosition + 1 <= GameManager.Instance.levelsUnlocked - 1)
                {
                    charBezierFollow.MoveOnRoute(arrayPosition);
                    arrayPosition += 1;

                }
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameManager.Instance.currentScene = levels[arrayPosition];
                SceneLoader.Instance.LoadLevelInstant(levels[arrayPosition].ToString());
            }
        }
    }
}

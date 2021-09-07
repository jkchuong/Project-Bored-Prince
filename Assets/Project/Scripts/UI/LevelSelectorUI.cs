using System;
using System.Collections.Generic;
using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class LevelSelectorUI : MonoBehaviour
    {
        [SerializeField] private List<Scenes> levels;
        [SerializeField] private List<Transform> levelPositions;
        [SerializeField] private Image character;

        private int arrayPosition;
        
        private void Start()
        {
            arrayPosition = levels.IndexOf(GameManager.Instance.currentScene);
            MoveCharacterToLevelPosition(arrayPosition);
        }

        private void Update()
        {
            // Press A move left of array
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (arrayPosition - 1 >= 0)
                {
                    arrayPosition -= 1;
                }
                
                MoveCharacterToLevelPosition(arrayPosition);
            }
            
            // Press D move right of array
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (arrayPosition + 1 < levels.Count && arrayPosition + 1 <= GameManager.Instance.levelsUnlocked - 1)
                {
                    arrayPosition += 1;
                }
                
                MoveCharacterToLevelPosition(arrayPosition);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                GameManager.Instance.currentScene = levels[arrayPosition];
                SceneLoader.Instance.LoadLevelInstant(levels[arrayPosition].ToString());
            }
        }

        private void MoveCharacterToLevelPosition(int position)
        {
            // TODO: Animate character movement
            character.transform.position = levelPositions[position].position;
        }
    }
}

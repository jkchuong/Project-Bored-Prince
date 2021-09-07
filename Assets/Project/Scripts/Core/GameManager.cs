using UnityEngine;

/*
 * Track Level Selection
 * Track last selected level
 * Track Questing Progress
 * Track player score
 * Track coins collected for current level
 */

namespace Project.Scripts.Core
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        public Scenes currentScene = Scenes.Prototype1;
        public int levelsUnlocked = 1;
    }
}

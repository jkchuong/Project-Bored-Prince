using UnityEngine;

namespace Project.Scripts.Collectibles
{
   [CreateAssetMenu(menuName = "Quest Item", fileName = "New Quest Item")] 
    public class QuestItemScriptable : ScriptableObject
    {
        public string itemName;
        public Sprite itemSprite;
        public Sprite itemImageUI;
    }
}

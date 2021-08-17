using UnityEngine;

namespace Project.Scripts.Collectibles
{
   [CreateAssetMenu(menuName = "Quest Item", fileName = "New Quest Item")] 
    public class ItemScriptable : ScriptableObject
    {
        public ItemType itemType;
        public string itemName;
        public Sprite itemSprite;
        public Sprite itemImageUI;
    }
}

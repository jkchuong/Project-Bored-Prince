using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Collectibles;
using UnityEngine;

namespace Project.Scripts.Character
{
    [Serializable]
    public class Inventory
    {
        public event Action<int> OnCoinsChanged;
        public event Action<Sprite> OnLevelItemCollect;
        public event Action<Sprite> OnQuestItemCollect;
        
        [SerializeField] private int coinsCollected;
        private List<ItemScriptable> questItems = new List<ItemScriptable>();
        private List<ItemScriptable> levelItems = new List<ItemScriptable>();

        public void AddCoin()
        {
            coinsCollected++;
            OnCoinsChanged?.Invoke(coinsCollected);
        }
        
        public void AddItem(ItemScriptable item)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (item.itemType)
            {
                case ItemType.QuestItem:
                    questItems.Add(item);
                    OnQuestItemCollect?.Invoke(item.itemImageUI);
                    break;
                
                case ItemType.LevelItem:
                    levelItems.Add(item);
                    OnLevelItemCollect?.Invoke(item.itemImageUI);
                    break;

                default:
                    return;
            }
        }

        public void RemoveItem(ItemScriptable item)
        {
            if (!InventoryContains(item))
                return;

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (item.itemType)
            {
                case ItemType.QuestItem:
                    questItems.Remove(item);
                    break;
                
                case ItemType.LevelItem:
                    levelItems.Remove(item);
                    break;

                default:
                    return;
            }
        }

        public bool InventoryContains(ItemScriptable item)
        {
            return item.itemType switch
            {
                ItemType.QuestItem => questItems.Contains(item),
                ItemType.LevelItem => levelItems.Contains(item),
                _ => false
            };
        }

        public bool InventoryContains(string itemName)
        {
            return levelItems.Any(item => item.itemName == itemName);
        }
    }
}

using System;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class QuestItem : Collectible
    {
        public override ItemType ItemType => ItemType.QuestItem;

        [SerializeField] private QuestItemScriptable questItemScriptable;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = questItemScriptable.itemSprite;
        }

        protected override void HandlePickUp(Player player)
        {
            player.AddQuestItem(questItemScriptable.itemName, questItemScriptable.itemImageUI);
            
            Destroy(gameObject);
        }
    }
}

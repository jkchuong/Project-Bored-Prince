using System;
using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class QuestItem : Collectible
    {
        public override ItemType ItemType => ItemType.QuestItem;

        [SerializeField] private ItemScriptable itemScriptable;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = itemScriptable.itemSprite;
        }

        protected override void HandlePickUp(Player player)
        {
            player.inventory.AddItem(itemScriptable);
            
            Destroy(gameObject);
        }
    }
}

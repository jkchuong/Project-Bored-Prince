using System;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Buff : Collectible
    {
        public override ItemType ItemType => ItemType.Buff;
        
        public abstract BuffType BuffType { get; }

        [SerializeField] protected Sprite buffItemSprite;
        [SerializeField] protected Sprite buffImageUI;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = buffItemSprite;
        }

        protected override void HandlePickUp(Player player)
        {
            player.AddBuff(BuffAbility, buffImageUI);
            
            Destroy(gameObject);
        }
        
        protected abstract void BuffAbility();
    }
    
    public enum BuffType
    {
        Fire,
        Ice,
        Vine
    }
}

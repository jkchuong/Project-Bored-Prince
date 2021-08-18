using System;
using Project.Scripts.Character;
using Project.Scripts.Enemy;
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
            player.AddBuff(this, buffImageUI);
            
            DeactivateItem();
        }

        private void DeactivateItem()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        
        public abstract void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration);
    }
    
    public enum BuffType
    {
        Blank,
        Fire,
        Ice,
        Vine
    }
}

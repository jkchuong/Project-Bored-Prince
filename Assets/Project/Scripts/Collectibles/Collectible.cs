using System;
using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public abstract class Collectible : MonoBehaviour
    { 
        public abstract ItemType ItemType { get; }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            HandlePickUp(other.GetComponent<Player>());
        }

        protected abstract void HandlePickUp(Player player);
    }
    
    public enum ItemType
    {
        Coin,
        Health,
        Buff,
        QuestItem,
        LevelItem
    }
}

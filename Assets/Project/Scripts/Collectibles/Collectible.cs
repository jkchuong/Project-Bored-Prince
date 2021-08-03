using System;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public abstract class Collectible : MonoBehaviour
    { 
        [SerializeField] private ItemType itemType;
    
        private enum ItemType
        {
            Coin,
            Health,
            Buff,
            QuestItem
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            OnPickUp(other.GetComponent<Player>());
        }

        protected abstract void OnPickUp(Player player);
    }
}

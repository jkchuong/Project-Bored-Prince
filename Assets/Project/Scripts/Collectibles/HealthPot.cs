using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class HealthPot : Collectible
    {
        public override ItemType ItemType => ItemType.Health;

        [SerializeField] private float healAmount = 10;
        
        protected override void HandlePickUp(Player player)
        {
            player.Health.ModifyHealth(healAmount);
            
            Destroy(gameObject);
        }
    }
}

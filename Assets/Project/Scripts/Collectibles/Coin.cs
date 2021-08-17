using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class Coin : Collectible
    {
        public override ItemType ItemType => ItemType.Coin;

        protected override void HandlePickUp(Player player)
        {
            player.inventory.AddCoin(); 
            
            Destroy(gameObject);
        }
    }
}

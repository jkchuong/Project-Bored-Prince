using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class Coin : Collectible
    {
        protected override void OnPickUp(Player player)
        {
            player.AddCoin(); 
            
            gameObject.SetActive(false);
        }
    }
}

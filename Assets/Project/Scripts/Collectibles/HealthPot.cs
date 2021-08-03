using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class HealthPot : Collectible
    {
        [SerializeField] private float healAmount = 10;
        
        protected override void OnPickUp(Player player)
        {
            player.ChangeHealth(healAmount);
            
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class HealthBuff : Collectible
    {
        [SerializeField] private float healAmount = 10;
        
        protected override void OnPickUp(Player player)
        {
            player.ChangeHealth(healAmount);
            
            gameObject.SetActive(false);
        }
    }
}

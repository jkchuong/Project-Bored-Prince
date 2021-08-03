using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public abstract class Buff : Collectible
    {
        public override ItemType ItemType => ItemType.Buff;
        
        public abstract BuffType BuffType { get; }

        protected override void HandlePickUp(Player player)
        {
            player.AddBuff(BuffAbility);
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

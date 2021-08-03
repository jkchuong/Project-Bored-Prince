using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public abstract class Buff : Collectible
    {
        [SerializeField] private BuffType buffType;

        private enum BuffType
        {
            Fire,
            Ice,
            Vine
        }
            
        // TODO: Add buffs
        protected abstract void BuffAbility();
    }
}

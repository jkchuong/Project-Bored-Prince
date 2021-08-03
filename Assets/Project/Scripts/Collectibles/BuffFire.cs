using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        protected override void BuffAbility()
        {
            Debug.Log("Fire Ability Activated");
        }
    }
}

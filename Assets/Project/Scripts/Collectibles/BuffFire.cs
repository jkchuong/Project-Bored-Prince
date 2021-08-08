using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        protected override void BuffAbility(LeftRightEnemy leftRightEnemy)
        {
            Debug.Log("Fire Ability Activated on " + leftRightEnemy.name);
        }
    }
}

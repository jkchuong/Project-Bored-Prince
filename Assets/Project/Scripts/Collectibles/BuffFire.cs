using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        protected override void BuffAbility(Enemy enemy)
        {
            Debug.Log("Fire Ability Activated on " + enemy.name);
        }
    }
}

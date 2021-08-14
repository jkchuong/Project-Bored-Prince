using System.Collections;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        protected override void BuffAbility(Enemy enemy, float effectFactor, float effectDuration)
        {
            Debug.Log("Fire Ability Activated on " + enemy.name);
            // StartCoroutine(FireAttack(enemy));
        }

        private void FireAttack(Enemy enemy)
        {
            // TODO: Have total damage over time in inspector?
        }
    }
}

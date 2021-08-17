using System.Collections;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        protected override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            Debug.Log("Fire Ability Activated on " + enemyObject.name);
            // StartCoroutine(FireAttack(enemy));
        }

        private void FireAttack(EnemyObject enemyObject)
        {
            // TODO: Have total damage over time in inspector?
        }
    }
}

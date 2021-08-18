using System;
using System.Collections;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffFire : Buff
    {
        public override BuffType BuffType => BuffType.Fire;

        public override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            StartCoroutine(FireAttack(enemyObject, effectFactor * 8, effectDuration));
        }

        private IEnumerator FireAttack(EnemyObject enemyObject, float totalDamage, float duration)
        {
            const float applyEveryNSecond = 0.3f;
            int totalAppliedTimes = (int)Math.Ceiling(duration / applyEveryNSecond);
            int appliedTimes = 0;
            float damage = totalDamage * (applyEveryNSecond / duration);

            while (appliedTimes < totalAppliedTimes)
            {
                enemyObject.Health.ModifyHealth(-damage);
                yield return new WaitForSeconds(applyEveryNSecond);
                appliedTimes++;
            }
        }
    }
}

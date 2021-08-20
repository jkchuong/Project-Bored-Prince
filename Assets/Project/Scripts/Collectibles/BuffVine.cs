using System;
using System.Collections;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffVine : Buff
    {
        public override BuffType BuffType => BuffType.Vine;

        public override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            StartCoroutine(VineAttack(enemyObject, effectFactor * 2, effectDuration));
        }

        private IEnumerator VineAttack(EnemyObject enemyObject, float effectFactor, float duration)
        {
            float originalMaxSpeed = enemyObject.maxSpeed;

            enemyObject.maxSpeed = originalMaxSpeed / (effectFactor + 1);
            
            const float applyEveryNSecond = 0.3f;
            int totalAppliedTimes = (int)Math.Ceiling(duration / applyEveryNSecond);
            int appliedTimes = 0;
            float damage = effectFactor * (applyEveryNSecond / duration);
            
            while (appliedTimes < totalAppliedTimes)
            {
                enemyObject.Health.ModifyHealth(-damage);
                yield return new WaitForSeconds(applyEveryNSecond);
                appliedTimes++;
            }

            enemyObject.maxSpeed = originalMaxSpeed;
        }
    }
}
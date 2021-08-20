using System.Collections;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffIce : Buff
    {
        public override BuffType BuffType => BuffType.Ice;

        public override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            StartCoroutine(IceAttack(enemyObject, effectFactor, effectDuration));
        }

        private IEnumerator IceAttack(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            float originalMaxSpeed = enemyObject.maxSpeed;
            enemyObject.maxSpeed = 0;
            yield return new WaitForSeconds(effectDuration);
            enemyObject.maxSpeed = originalMaxSpeed;
        }
    }
}
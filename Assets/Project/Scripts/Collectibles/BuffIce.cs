using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffIce : Buff
    {
        public override BuffType BuffType => BuffType.Ice;

        public override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            Debug.Log("Ice Ability Activated on " + enemyObject.name);
        }
    }
}
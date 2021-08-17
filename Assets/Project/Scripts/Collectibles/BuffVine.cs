using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffVine : Buff
    {
        public override BuffType BuffType => BuffType.Vine;

        protected override void BuffAbility(EnemyObject enemyObject, float effectFactor, float effectDuration)
        {
            Debug.Log("Vine Ability Activated on " + enemyObject.name);
        }
    }
}
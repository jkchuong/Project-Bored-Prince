using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffIce : Buff
    {
        public override BuffType BuffType => BuffType.Ice;

        protected override void BuffAbility(Enemy enemy, float effectFactor, float effectDuration)
        {
            Debug.Log("Ice Ability Activated on " + enemy.name);
        }
    }
}
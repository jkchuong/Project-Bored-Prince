using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffVine : Buff
    {
        public override BuffType BuffType => BuffType.Vine;

        protected override void BuffAbility(LeftRightEnemy leftRightEnemy)
        {
            Debug.Log("Vine Ability Activated on " + leftRightEnemy.name);
        }
    }
}
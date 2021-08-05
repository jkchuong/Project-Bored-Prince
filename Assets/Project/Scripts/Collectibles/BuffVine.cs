using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffVine : Buff
    {
        public override BuffType BuffType => BuffType.Vine;

        protected override void BuffAbility(Enemy enemy)
        {
            Debug.Log("Vine Ability Activated on " + enemy.name);
        }
    }
}
using UnityEngine;

namespace Project.Scripts.Collectibles
{
    public class BuffIce : Buff
    {
        public override BuffType BuffType => BuffType.Ice;

        protected override void BuffAbility()
        {
            Debug.Log("Ice Ability Activated");
        }
    }
}
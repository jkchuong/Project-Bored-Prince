using System.Collections;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class DashEnemy : AttackerEnemy
    { 
        protected override IEnumerator DoAttack()
        {
            Debug.Log(" Do Attack");
            yield break;
        }
    }
}

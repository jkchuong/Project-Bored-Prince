using System.Collections;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class DashEnemy : AttackerEnemy
    { 
        protected override IEnumerator DoAttack()
        {
            Debug.Log(gameObject.name + " Do Attack");
            yield break;
        }
    }
}

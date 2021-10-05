using System.Collections;
using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class BomberEnemy : AttackerEnemy
    {
        protected override IEnumerator DoAttack()
        {
            isAttacking = true;
            
            yield return new WaitForSeconds(2f);

            player.Health.ModifyHealth(-10f);
            
            gameObject.SetActive(false);
            
            isAttacking = false;
        }
    }
}

using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Character
{
    public class AttackBox : MonoBehaviour
    {
        public float DamageAmount { private get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyObject enemyObject = other.GetComponent<EnemyObject>();

            if (enemyObject)
            {
                enemyObject.Health.ModifyHealth(-DamageAmount);
            }
        }
    }
}

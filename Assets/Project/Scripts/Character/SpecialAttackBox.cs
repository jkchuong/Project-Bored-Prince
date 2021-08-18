using System;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Character
{
    public class SpecialAttackBox : MonoBehaviour
    {
        [Tooltip("How strong the ability is, 0 is weak and 1 is strong")]
        [Range(0, 1)]
        [SerializeField] private float effectFactor = 1f;
    
        [Tooltip("Duration of the effect")]
        [SerializeField] private float effectDuration = 3f;
    
        private event Action<EnemyObject, float, float> Buff;

        private Collider2D attackCollider;

        private void Awake()
        {
            attackCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyObject enemyObject = other.GetComponent<EnemyObject>();

            if (enemyObject)
            {
                Buff?.Invoke(enemyObject, effectFactor, effectDuration);
            }
        }

        public void SetBuff(Action<EnemyObject, float, float> buffToSet)
        {
            Buff = buffToSet;
        }

        public void SetAttackSize(float radius)
        {
            GetComponent<CircleCollider2D>().radius = radius;
        }

        public void SetAttack(bool state)
        {
            attackCollider.enabled = state;
        }
    }
}

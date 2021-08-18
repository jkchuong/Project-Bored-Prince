using System;
using Project.Scripts.Collectibles;
using Project.Scripts.Enemy;
using UnityEngine;

namespace Project.Scripts.Character
{
    public class SpecialAttackBox : MonoBehaviour
    {
        [Tooltip("Effect multiplier. 0 is no effect.")]
        [Range(0, 10)]
        [SerializeField] private float effectFactor = 1f;
    
        [Tooltip("Duration of the effect")]
        [SerializeField] private float effectDuration = 3f;
    
        private Buff buff;

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
                buff.BuffAbility(enemyObject, effectFactor, effectDuration);
            }
        }

        public void SetBuff(Buff buffToSet)
        {
            buff = buffToSet;
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

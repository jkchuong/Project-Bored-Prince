using Project.Scripts.Character;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class EnemyObject : PhysicsObject
    {
        [Header("Stats")]
        public float maxSpeed = 1f;
        [SerializeField] private float damageAmount = 5;

        [SerializeField] private float isFrozen;
    
        public Health Health { get; private set; }

        private void Awake()
        {
            Health = GetComponent<Health>();
            Health.DoDeath += HealthOnDoDeath;
        }

        protected virtual void HealthOnDoDeath()
        {
            gameObject.SetActive(false);
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player)
            {
                player.Health.ModifyHealth(-damageAmount);
            }
        }
    }
}

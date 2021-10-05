using System;
using System.Collections;
using Project.Scripts.Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Enemy
{
    public abstract class AttackerEnemy : LeftRightEnemy
    {
        [SerializeField] private float detectionRange = 5f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float chaseSpeed = 2f;

        private Player player;

        private float distanceToPlayer;
        
        private bool coroutineAllowed = true;
        private bool idleMoving;
        
        private protected override void Start()
        {
            base.Start();
            player = FindObjectOfType<Player>();
        }

        protected override void Update()
        {
            distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(DoAttack());
                return;
            }

            if (distanceToPlayer <= detectionRange)
            {
                Chase();
                return;
            }

            if (idleMoving)
            {
                base.Update();
            }
            else
            {
                targetVelocity = new Vector2(0, 0);
            }

            if (coroutineAllowed)
            {
                StartCoroutine( DoIdle(Random.Range(2f, 4f)));
            }
        }

        private IEnumerator DoIdle(float idleDuration)
        {
            coroutineAllowed = false;
            
            yield return new WaitForSeconds(idleDuration);

            idleMoving = !idleMoving;

            direction = Random.value < 0.5f ? 1 : -1;

            coroutineAllowed = true;
        }

        private void Chase()
        {
            direction = player.transform.position.x < transform.position.x ? -1 : 1;
            targetVelocity = new Vector2(chaseSpeed * direction, 0);
        }
        
        protected abstract IEnumerator DoAttack();

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        
#endif
        
    }
}

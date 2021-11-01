using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Collectibles;
using Project.Scripts.Core;
using Project.Scripts.Enemy;
using Project.Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Character
{
    public class Player : PhysicsObject
    {
        [Header("Movement")]
        [SerializeField] private float maxSpeed = 5f;
        [SerializeField] private float jumpSpeed = 10f;

        [Header("Combat")]
        [SerializeField] private float attackDamage;
        [SerializeField] private float specialAttackRadius = 1f;

        [Header("Stats")]
        [SerializeField] private float specialAttackSpeed = 0.2f;
        [SerializeField] private float attackSpeed = 0.2f;
        public Health Health { get; private set; }
        public readonly Inventory inventory = new Inventory();
    
        public Vector2 Checkpoint { private get; set; }
        [SerializeField] private Sprite blankBuffSprite;
        
        private AttackBox attackBox;
        private SpecialAttackBox specialAttackBox;
        private bool regularAttacking;
        private bool specialAttacking;
        private Animator animator;
        private static readonly int Speed = Animator.StringToHash("speed");
        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int Grounded2 = Animator.StringToHash("jump");

        public event Action<Sprite> OnBuffChanged;

        private void Awake()
        {
            // Get and deactivate attack box
            attackBox = GetComponentInChildren<AttackBox>();

            // Get and deactivate attack box
            specialAttackBox = GetComponentInChildren<SpecialAttackBox>();

            Checkpoint = transform.position;
            
            Health = GetComponent<Health>();

            animator = GetComponent<Animator>();
        }

        private protected override void Start()
        {
            base.Start();

            // Bind player to UI and reset UI 
            FindObjectOfType<GameUI>()?.BindPlayer(this);
            Health.ResetHealth();
            AddBuff(null, blankBuffSprite);

            attackBox.DamageAmount = attackDamage;
            attackBox.gameObject.SetActive(false);
            
            specialAttackBox.SetAttackSize(specialAttackRadius);
            specialAttackBox.SetAttack(false);
            
            Health.DoDeath += HealthOnDoDeath;
        }
        
        private void Update()
        {
            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

            if (Input.GetButton("Jump") && grounded)
            {
                velocity.y = jumpSpeed;
                animator.SetTrigger(Grounded2);
            }

            // TODO: Modify this in animator
            if (targetVelocity.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (targetVelocity.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }

            if (Input.GetKeyDown(KeyCode.K) && !regularAttacking)
            {
                StartCoroutine(RegularAttack());
            }

            if (Input.GetKeyDown(KeyCode.O) && !specialAttacking)
            {
                StartCoroutine(SpecialAttack());
            }
            
            animator.SetFloat(Speed, Mathf.Abs(velocity.x));
            animator.SetBool(Grounded, grounded);
        }

        private void HealthOnDoDeath()
        {
            // TODO: Player death animation
            
            StartCoroutine(ResetPlayer());
        }

        private IEnumerator ResetPlayer()
        {
            // Move loading screen canvas
            if (!SceneLoader.Instance)
                yield break;
            
            SceneLoader.Instance.StartLoadingScreen();
            
            // Wait for loading screen
            yield return new WaitForSeconds(1f);
            
            // Move player to last checkpoint and reset stats
            transform.position = Checkpoint;
            Health.ResetHealth();
            AddBuff(null, blankBuffSprite);
            
            // Remove some coins
            float randomPercentage = Random.Range(0.1f, 0.3f);
            int coinsToDrop = Mathf.FloorToInt(inventory.coinsCollected * randomPercentage);
            inventory.ModifyCoin(-coinsToDrop);

            // Remove loading screen
            SceneLoader.Instance.EndLoadingScreen();
        }

        private IEnumerator SpecialAttack()
        {
            // TODO: Match this to animator but using Animation Event to trigger - set true during swing and false end of swing
            specialAttackBox.SetAttack(true);
            specialAttacking = true;
            yield return new WaitForSeconds(specialAttackSpeed);
            specialAttackBox.SetAttack(false);
            specialAttacking = false;
        }

        private IEnumerator RegularAttack()
        {
            // TODO: Match this to animator but using Animation Event to trigger - set true during swing and false end of swing
            attackBox.gameObject.SetActive(true);
            regularAttacking = true;
            yield return new WaitForSeconds(attackSpeed);
            attackBox.gameObject.SetActive(false);
            regularAttacking = false;
        }

        public void AddBuff(Buff buff, Sprite buffSprite)
        {
            specialAttackBox.SetBuff(buff);
            OnBuffChanged?.Invoke(buffSprite);
        }

#if UNITY_EDITOR
    
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, specialAttackRadius);
        }
    
#endif
    
    }
}

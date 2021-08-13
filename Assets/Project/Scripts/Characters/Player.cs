using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Collectibles;
using Project.Scripts.UI;
using UnityEngine;

public class Player : PhysicsObject
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float specialAttackRadius = 1f;

    [Header("Stats")]
    [SerializeField] private int coinsCollected;
    [SerializeField] private BuffType buffType = BuffType.Blank;
    public Health Health { get; private set; }
    private Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    
    private AttackBox attackBox;
    private SpecialAttackBox specialAttackBox;
    private bool regularAttacking;
    private bool specialAttacking;

    public event Action<int> OnCoinsChanged;
    public event Action<Sprite> OnBuffChanged;
    public event Action<Sprite> OnInventoryChanged;

    private void Awake()
    {
        // Get and deactivate attack box
        attackBox = GetComponentInChildren<AttackBox>();
        attackBox.DamageAmount = attackDamage;
        attackBox.gameObject.SetActive(false);
        
        // Get and deactivate attack box
        specialAttackBox = GetComponentInChildren<SpecialAttackBox>();
        specialAttackBox.SetAttackSize(specialAttackRadius);
        specialAttackBox.gameObject.SetActive(false);

        Health = GetComponent<Health>();
    }

    private protected override void Start()
    {
        base.Start();

        // Bind player to UI
        FindObjectOfType<GameUI>()?.BindPlayer(this);
        
        Health.DoDeath += HealthOnDoDeath;
    }

    private void HealthOnDoDeath()
    {
        SceneLoader.Instance.ReloadScene();
    }

    private void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = jumpSpeed;
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
    }

    private IEnumerator SpecialAttack()
    {
        // TODO: Match this to animator but using Animation Event to trigger - set true during swing and false end of swing
        specialAttackBox.gameObject.SetActive(true);
        specialAttacking = true;
        yield return new WaitForSeconds(0.2f);
        specialAttackBox.gameObject.SetActive(false);
        specialAttacking = false;
    }

    private IEnumerator RegularAttack()
    {
        // TODO: Match this to animator but using Animation Event to trigger - set true during swing and false end of swing
        attackBox.gameObject.SetActive(true);
        regularAttacking = true;
        yield return new WaitForSeconds(0.2f);
        attackBox.gameObject.SetActive(false);
        regularAttacking = false;
    }

    public void AddCoin()
    {
        coinsCollected++;
        OnCoinsChanged?.Invoke(coinsCollected);
    }
    
    public void AddQuestItem(string itemName, Sprite itemSprite)
    {
        inventory.Add(itemName, itemSprite);
        OnInventoryChanged?.Invoke(itemSprite);
    }
    
    public void RemoveQuestItem(string itemName)
    {
        inventory.Remove(itemName);
        OnInventoryChanged?.Invoke(null);
    }
    
    public void AddBuff(Action<LeftRightEnemy> buffAbility, Sprite buffSprite, BuffType buffType)
    {
        specialAttackBox.SetBuff(buffAbility);
        this.buffType = buffType;
        OnBuffChanged?.Invoke(buffSprite);
    }

    public bool InventoryContains(string itemName)
    {
        return inventory.ContainsKey(itemName);
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, specialAttackRadius);
    }
    
#endif
    
}

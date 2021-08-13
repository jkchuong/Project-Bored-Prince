using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Collectibles;
using Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : PhysicsObject
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Combat")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float specialAttackRadius = 1f;

    [Header("Stats")]
    [SerializeField] private float healthPercentage;
    [SerializeField] private int coinsCollected;
    [SerializeField] private BuffType buffType = BuffType.Blank;

    private Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    
    private AttackBox attackBox;
    private SpecialAttackBox specialAttackBox;
    private bool regularAttacking;
    private bool specialAttacking;

    public event Action<int> OnCoinsChanged;
    public event Action<float> OnHealthChanged;
    public event Action<Sprite> OnBuffChanged;
    public event Action<Sprite> OnInventoryChanged;
    
    private protected override void Start()
    {
        base.Start();

        // Get and deactivate attack box
        attackBox = GetComponentInChildren<AttackBox>();
        attackBox.DamageAmount = attackDamage;
        attackBox.gameObject.SetActive(false);
        
        // Get and deactivate attack box
        specialAttackBox = GetComponentInChildren<SpecialAttackBox>();
        specialAttackBox.SetAttackSize(specialAttackRadius);
        specialAttackBox.gameObject.SetActive(false);
        
        // Bind player to UI
        FindObjectOfType<GameUI>()?.BindPlayer(this);
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        OnHealthChanged?.Invoke(healthPercentage);
        OnCoinsChanged?.Invoke(coinsCollected);
        //TODO: Pass buff information and inventory information
        // OnBuffChanged?.Invoke()
        // OnInventoryChanged?.Invoke();
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
    
    private void Die()
    {
        SceneLoader.Instance.ReloadScene();
    }

    public void AddCoin()
    {
        coinsCollected++;
        OnCoinsChanged?.Invoke(coinsCollected);
    }
    
    public void ModifyHealth(float healthChange)
    {
        healthPercentage = Mathf.Clamp(healthPercentage + healthChange, 0, 100);
        OnHealthChanged?.Invoke(healthPercentage);
        
        if (healthPercentage <= 0)
        {
            Die();
        }
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

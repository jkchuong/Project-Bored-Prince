using System;
using System.Collections;
using System.Collections.Generic;
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

    private Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();

    private const float HEALTH_MAX_PERCENTAGE = 100f;
    private Vector2 fullHealthBarSize;
    
    private AttackBox attackBox;
    private SpecialAttackBox specialAttackBox;
    private bool regularAttacking;
    private bool specialAttacking;
    
    private protected override void Start()
    {
        base.Start();

        // Get original health bar size
        // fullHealthBarSize = healthBar.rectTransform.sizeDelta;

        // Get and deactivate attack box
        attackBox = GetComponentInChildren<AttackBox>();
        attackBox.DamageAmount = attackDamage;
        attackBox.gameObject.SetActive(false);
        
        // Get and deactivate attack box
        specialAttackBox = GetComponentInChildren<SpecialAttackBox>();
        specialAttackBox.SetAttackSize(specialAttackRadius);
        specialAttackBox.gameObject.SetActive(false);
        
        // UpdateUI();
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

    // private void UpdateUI()
    // {
    //     // Update Coins
    //     coinsText.text = "Coins: " + coinsCollected;
    //
    //     // Update Health
    //     float healthLength = healthPercentage / HEALTH_MAX_PERCENTAGE;
    //     healthBar.rectTransform.sizeDelta = new Vector2(fullHealthBarSize.x * healthLength, fullHealthBarSize.y);
    // }
    
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
        // UpdateUI();
    }
    
    public void ModifyHealth(float healthChange)
    {
        healthPercentage = Mathf.Clamp(healthPercentage + healthChange, 0, 100);
        // UpdateUI();
    
        if (healthPercentage <= 0)
        {
            Die();
        }
    }

    public void AddQuestItem(string itemName, Sprite itemSprite)
    {
        inventory.Add(itemName, itemSprite);
        // inventoryItemImage.sprite = inventory[itemName];
    }
    
    public void RemoveQuestItem(string itemName)
    {
        inventory.Remove(itemName);
        // inventoryItemImage.sprite = inventoryBlankItem;
    }
    
    public void AddBuff(Action<LeftRightEnemy> buffAbility, Sprite buffSprite)
    {
        specialAttackBox.SetBuff(buffAbility);
        // buffImage.sprite = buffSprite;
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

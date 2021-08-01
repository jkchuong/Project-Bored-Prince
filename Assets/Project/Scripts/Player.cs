using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : PhysicsObject
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Stats")]
    [SerializeField] private float healthPercentage;
    [SerializeField] private int coinsCollected;

    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Image healthBar;

    private const float HEALTH_MAX_PERCENTAGE = 100f;
    private Vector2 fullHealthBarSize;
    
    private protected override void Start()
    {
        base.Start();
        rb2d.gravityScale = 0;

        fullHealthBarSize = healthBar.rectTransform.sizeDelta;
        
        UpdateUI();
    }

    private void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = jumpSpeed;
        }
        
        ChangeHealth(1);
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateUI();
    }

    public void ChangeHealth(float healthChange)
    {
        healthPercentage = Mathf.Clamp(healthPercentage + healthChange, 0, 100);

        UpdateUI();
    }
    
    private void UpdateUI()
    {
        coinsText.text = "Coins: " + coinsCollected;

        float healthLength = healthPercentage / HEALTH_MAX_PERCENTAGE;
        healthBar.rectTransform.sizeDelta = new Vector2(fullHealthBarSize.x * healthLength, fullHealthBarSize.y);
    }
}

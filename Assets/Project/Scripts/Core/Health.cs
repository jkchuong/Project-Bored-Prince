using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    public event Action<float> OnHealthChanged;
    public event Action DoDeath;

    private float HealthPercentage => health / maxHealth;

    private void Start()
    {
        health = maxHealth;
    }
    
    public void ModifyHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(HealthPercentage);

        if (HealthPercentage <= 0)
        {
            DoDeath?.Invoke();
        }
    }
}

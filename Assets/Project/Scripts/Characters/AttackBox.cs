using System;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public float DamageAmount { private get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemy.Health.ModifyHealth(-DamageAmount);
        }
    }
}

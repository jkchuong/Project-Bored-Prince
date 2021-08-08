using System;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public float DamageAmount { private get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LeftRightEnemy leftRightEnemy = other.GetComponent<LeftRightEnemy>();

        if (leftRightEnemy)
        {
            leftRightEnemy.ModifyHealth(-DamageAmount);
        }
    }
}

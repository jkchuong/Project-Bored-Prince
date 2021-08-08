using System;
using Project.Scripts.Collectibles;
using UnityEngine;

public class SpecialAttackBox : MonoBehaviour
{
    private event Action<LeftRightEnemy> Buff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        LeftRightEnemy leftRightEnemy = other.GetComponent<LeftRightEnemy>();

        if (leftRightEnemy)
        {
            Buff?.Invoke(leftRightEnemy);
        }
    }

    public void SetBuff(Action<LeftRightEnemy> buffToSet)
    {
        Buff = buffToSet;
    }

    public void SetAttackSize(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}

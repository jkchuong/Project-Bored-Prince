using System;
using Project.Scripts.Collectibles;
using UnityEngine;

public class SpecialAttackBox : MonoBehaviour
{
    private event Action Buff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            Buff?.Invoke();
        }
    }

    public void SetBuff(Action buffToSet)
    {
        Buff = buffToSet;
    }

    public void SetAttackSize(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}

using System;
using Project.Scripts.Collectibles;
using UnityEngine;

public class SpecialAttackBox : MonoBehaviour
{
    private event Action<Enemy> Buff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            Buff?.Invoke(enemy);
        }
    }

    public void SetBuff(Action<Enemy> buffToSet)
    {
        Buff = buffToSet;
    }

    public void SetAttackSize(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}

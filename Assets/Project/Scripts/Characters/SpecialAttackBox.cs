using System;
using Project.Scripts.Collectibles;
using UnityEngine;

public class SpecialAttackBox : MonoBehaviour
{
    [Tooltip("How strong the ability is, 0 is weak and 1 is strong")]
    [SerializeField] private float effectFactor = 1f;
    
    [Tooltip("Duration of the effect")]
    [SerializeField] private float effectDuration = 3f;
    
    private event Action<Enemy, float, float> Buff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            Buff?.Invoke(enemy, effectFactor, effectDuration);
        }
    }

    public void SetBuff(Action<Enemy, float, float> buffToSet)
    {
        Buff = buffToSet;
    }

    public void SetAttackSize(float radius)
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }
}

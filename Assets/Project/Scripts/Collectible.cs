using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private enum ItemType
    {
        Coin,
        Health,
        FireBuff,
        IceBuff
    }

    [SerializeField] private ItemType itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            switch (itemType)
            {
                case ItemType.Coin:
                    player.AddCoin();
                    break;
                
                case ItemType.Health:
                    player.ChangeHealth(10f);
                    break;
                
                case ItemType.FireBuff: 
                    Debug.LogError("Fire Buff not implemented");
                    break;
                
                case ItemType.IceBuff:
                    Debug.LogError("Ice Buff not implemented");
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            gameObject.SetActive(false);
        }
    }
}

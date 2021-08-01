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
            other.GetComponent<Player>().AddCoin(); 
            
            gameObject.SetActive(false);
        }
    }
}

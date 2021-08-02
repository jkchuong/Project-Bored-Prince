using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    
    private enum ItemType
    {
        Coin,
        Health,
        FireBuff,
        IceBuff,
        QuestItem
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            // TODO: Add Event Listener instead?
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

                case ItemType.QuestItem:
                    player.AddInventoryItem(itemName, itemSprite);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            gameObject.SetActive(false);
        }
    }
}

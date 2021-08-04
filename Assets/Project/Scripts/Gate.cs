using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string gateUnlockItem;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if (player.InventoryContains(gateUnlockItem))
            {
                player.RemoveQuestItem(gateUnlockItem);
                Destroy(gameObject);
            }
        }
    }
}

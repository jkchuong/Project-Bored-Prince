using System;
using Project.Scripts.Character;
using Project.Scripts.Collectibles;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private ItemScriptable gateUnlockItem;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if (player.inventory.InventoryContains(gateUnlockItem))
            {
                player.inventory.RemoveItem(gateUnlockItem);
                Destroy(gameObject);
            }
        }
    }
}

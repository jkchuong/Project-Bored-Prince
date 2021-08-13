using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image buffIcon;
        [SerializeField] private Image inventoryItem;

        [SerializeField] private Sprite blankBuffIcon;
        [SerializeField] private Sprite blankInventoryItem;
        
        private Vector2 fullHealthBarSize;
        
        private static Player boundPlayer;
        
        private void OnEnable()
        { 
            fullHealthBarSize = healthBar.rectTransform.sizeDelta;

            BindPlayer(FindObjectOfType<Player>());
            
            if (!boundPlayer)
                return;
        }

        private void OnDestroy()
        {
            DeregisterPlayerUI();
        }

        public void BindPlayer([CanBeNull] Player player)
        {
            DeregisterPlayerUI();
            boundPlayer = player;
            RegisterPlayerUI();
        }

        private void RegisterPlayerUI()
        {
            if (boundPlayer == null)
                return;
            
            boundPlayer.Health.OnHealthChanged += HandleHealthChanged;
            boundPlayer.OnCoinsChanged += HandleCoinChanged;
            boundPlayer.OnBuffChanged += HandleBuffChanged;
            boundPlayer.OnInventoryChanged += HandleInventoryChanged;
        }

        private void DeregisterPlayerUI()
        {
            if (boundPlayer == null)
                return;
            
            boundPlayer.Health.OnHealthChanged -= HandleHealthChanged;
            boundPlayer.OnCoinsChanged -= HandleCoinChanged;
            boundPlayer.OnBuffChanged -= HandleBuffChanged;
        }

        private void HandleHealthChanged(float healthPercentage)
        {
            healthBar.rectTransform.sizeDelta = new Vector2(fullHealthBarSize.x * healthPercentage, fullHealthBarSize.y);
        }

        private void HandleCoinChanged(int coinAmount)
        {
            coinText.text = "Coins: " + coinAmount;
        }

        private void HandleBuffChanged(Sprite buffSprite)
        {
            buffIcon.sprite = buffSprite;
        }

        private void HandleInventoryChanged(Sprite itemSprite)
        {
            inventoryItem.sprite = itemSprite ? itemSprite : blankInventoryItem;
        }
    }
}

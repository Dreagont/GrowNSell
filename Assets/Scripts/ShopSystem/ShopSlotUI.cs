using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    public Image ItemIcon;
    public InventoryItemData InventoryItemData;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemPrice;
    private PlayerInventoryHolder playerInventoryHolder;
    private GameManager gameManager;
    void Start()
    {
        playerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        gameManager = FindObjectOfType<GameManager>();
        SetUi();
    }

    void Update()
    {
        
    }

    public void SetUi()
    {
        ItemIcon.sprite = InventoryItemData.icon;
        ItemName.text = InventoryItemData.displayName;
        ItemPrice.text = GlobalVariables.FormatNumber(InventoryItemData.buyPrice);
    }

    public void BuyItem()
    {
        if (playerInventoryHolder.AddToHotBar(InventoryItemData,1)) 
        {
            if (gameManager.CanAffordItem(InventoryItemData.buyPrice))
            {
                gameManager.Gold -= InventoryItemData.buyPrice; 
            }
        }
    }
}

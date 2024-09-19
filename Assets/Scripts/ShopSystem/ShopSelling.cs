using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelling : MonoBehaviour
{
    public Text GoldText;
    private PlayerInventoryHolder inventoryHolder;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = GlobalVariables.FormatNumber(gameManager.Gold);    
    }

    public void SellItem()
    {
        InventorySlots sellSlot = inventoryHolder.SellInventorySystem.InventorySlots[0];
        gameManager.Gold += sellSlot.ItemData.sellPrice * sellSlot.StackSize;
        sellSlot.RemoveFromStack(sellSlot.StackSize);
        Debug.Log("addded" + sellSlot.ItemData.sellPrice * sellSlot.StackSize);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSelling : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    private PlayerInventoryHolder inventoryHolder;

    void Start()
    {
        inventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = GlobalVariables.FormatNumber(GlobalVariables.Gold);    
    }

    public void SellItem()
    {
        InventorySlots sellSlot = inventoryHolder.SellInventorySystem.InventorySlots[0];
        GlobalVariables.Gold += sellSlot.ItemData.sellPrice * sellSlot.StackSize;
        sellSlot.RemoveFromStack(sellSlot.StackSize);
        Debug.Log("addded" + sellSlot.ItemData.sellPrice * sellSlot.StackSize);
    }
}

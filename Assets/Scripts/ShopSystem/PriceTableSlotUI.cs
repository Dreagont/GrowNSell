using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceTableSlotUI : MonoBehaviour
{
    public InventoryItemData ItemsPricing;
    public Image ItemIcon;
    public TextMeshProUGUI ItemSellPrice;
    public TextMeshProUGUI ItemName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePriceSlotUI();
    }

    public void UpdatePriceSlotUI()
    {
        ItemIcon.sprite = ItemsPricing.icon;
        ItemSellPrice.text =((int)ItemsPricing.GetTotalSellPrice()).ToString();
        ItemName.text = ItemsPricing.displayName;
    }
}

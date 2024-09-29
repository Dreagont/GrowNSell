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
    public TextMeshProUGUI ItemCurrentSellPrice;
    public TextMeshProUGUI ItemName;
    public GameManager GameManager;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager = FindAnyObjectByType<GameManager>();

    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePriceSlotUI(GameManager);
    }

    public void UpdatePriceSlotUI(GameManager gameManager)
    {
        
        ItemIcon.sprite = ItemsPricing.icon;
        //ItemSellPrice.text =((int)ItemsPricing.GetTotalSellPrice()).ToString();
        ItemSellPrice.text = ((int)gameManager.GetGoldAmount(ItemsPricing.minSellPrice)).ToString() + " - " + ((int)gameManager.GetGoldAmount(ItemsPricing.maxSellPrice)).ToString();
        ItemName.text = ItemsPricing.displayName;
        ItemCurrentSellPrice.text = ((int)gameManager.GetGoldAmount(ItemsPricing.sellPrice)).ToString();
    }
}

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
        if (ItemsPricing.itemType1 == ItemType.Material)
        {
            ItemSellPrice.text = ((int)gameManager.GetMaterialGoldAmount(ItemsPricing.minSellPrice)).ToString() + " - " + ((int)gameManager.GetMaterialGoldAmount(ItemsPricing.maxSellPrice)).ToString();
            ItemCurrentSellPrice.text = ((int)gameManager.GetMaterialGoldAmount(ItemsPricing.sellPrice)).ToString();


        }
        else if(ItemsPricing.itemType1 == ItemType.Crop) {
            ItemSellPrice.text = ((int)gameManager.GetCropGoldAmount(ItemsPricing.minSellPrice)).ToString() + " - " + ((int)gameManager.GetCropGoldAmount(ItemsPricing.maxSellPrice)).ToString();
            ItemCurrentSellPrice.text = ((int)gameManager.GetCropGoldAmount(ItemsPricing.sellPrice)).ToString();
        }
        else
        {
            ItemSellPrice.text = ((int)gameManager.GetCropGoldAmount(ItemsPricing.minSellPrice)).ToString() + " - " + ((int)gameManager.GetCropGoldAmount(ItemsPricing.maxSellPrice)).ToString();
            ItemCurrentSellPrice.text = ((int)gameManager.GetCropGoldAmount(ItemsPricing.sellPrice)).ToString();

        }
        ItemName.text = ItemsPricing.displayName;
    }
}

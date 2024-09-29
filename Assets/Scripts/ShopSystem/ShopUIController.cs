using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUIController : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject shopSlotPrefab;
    public GameObject priceSlot;
    public GameObject FadeScreen;

    public Transform shopSlotParent;
    public Transform PriceTableParent;

    public List<InventoryItemData> allAvailableItems;
    public List<ShopItem> itemsForSale;
    public List<InventoryItemData> PriceTable;

    public TextMeshProUGUI rollPriceText;
    public TextMeshProUGUI UpgradePriceText;
    public GameManager gameManager;

    public int shopLevel = 1;
    public int maxQuantityOfItems = 5;
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        ShopUI.gameObject.SetActive(false);
        RollNewItemsFree();
        PopulateShop();
        PopulatePriceTable();
    }

    private void Update()
    {
   
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
        }
        rollPriceText.text = "Roll:"+ gameManager.ShopRollCost.ToString();
        UpgradePriceText.text = gameManager.ShopUpgradeCost.ToString();
    }

    private void PopulateShop()
    {
        foreach (Transform child in shopSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ShopItem shopItem in itemsForSale)
        {
            GameObject slot = Instantiate(shopSlotPrefab, shopSlotParent);
            ShopSlotUI shopSlotUI = slot.GetComponent<ShopSlotUI>();
            shopSlotUI.InventoryItemData = shopItem.itemData;
            shopSlotUI.sellQuanity = shopItem.quantity;  
            shopSlotUI.SetUi();
        }
    }

    private void PopulatePriceTable()
    {
        foreach (var item in PriceTable)
        {
            GameObject slot = Instantiate(priceSlot, PriceTableParent);
            PriceTableSlotUI tableSlotUI = slot.GetComponent<PriceTableSlotUI>();
            tableSlotUI.ItemsPricing = item;
            tableSlotUI.UpdatePriceSlotUI(gameManager);
        }
    }

    private int GetNumberOfItemsForSale()
    {
        return (2 + shopLevel) < allAvailableItems.Count ? (2 + shopLevel) : allAvailableItems.Count;
    }

    public void RollNewItems()
    {
        if (gameManager.GoldManager.CanAffordItem(gameManager.ShopRollCost))
        {
            gameManager.GoldManager.Gold -= gameManager.ShopRollCost;
            gameManager.ShopRollCost *= 2;
            itemsForSale.Clear();

            int numberOfItems = GetNumberOfItemsForSale();

            for (int i = 0; i < numberOfItems; i++)
            {
                InventoryItemData randomItem = allAvailableItems[Random.Range(0, allAvailableItems.Count)];
                int randomQuantity = Random.Range(1, maxQuantityOfItems);

                itemsForSale.Add(new ShopItem(randomItem, randomQuantity));
            }

            PopulateShop();
        }
    }

    public void RollNewItemsFree()
    {
        
        itemsForSale.Clear();

        int numberOfItems = GetNumberOfItemsForSale();

        for (int i = 0; i < numberOfItems; i++)
        {
            InventoryItemData randomItem = allAvailableItems[Random.Range(0, allAvailableItems.Count)];
            int randomQuantity = Random.Range(1, maxQuantityOfItems);

            itemsForSale.Add(new ShopItem(randomItem, randomQuantity));
        }

        PopulateShop();
        
    }

    public void ToggleShopUI()
    {
        if (ShopUI.activeInHierarchy)
        {
            ShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
            FadeScreen.gameObject.SetActive(false);
        }
        else
        {
            ShopUI.SetActive(true);
            GlobalVariables.CanAction = false;
            FadeScreen.gameObject.SetActive(true);
        }
    }

    public void OnRollButtonPressed()
    {
        RollNewItems();
    }

    public void OnButtonUpgradePressed()
    {
        if (gameManager.GoldManager.CanAffordItem(gameManager.ShopUpgradeCost))
        {
            gameManager.GoldManager.Gold -= gameManager.ShopUpgradeCost;
            gameManager.ShopUpgradeCost *= 2;
            gameManager.ShopRollCost = 10;
            shopLevel += 1;

        }
    }
}

[System.Serializable]
public class ShopItem
{
    public InventoryItemData itemData;
    public int quantity;

    public ShopItem(InventoryItemData itemData, int quantity)
    {
        this.itemData = itemData;
        this.quantity = quantity;
    }
}

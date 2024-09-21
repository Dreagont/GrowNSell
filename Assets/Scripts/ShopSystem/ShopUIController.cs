using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUIController : MonoBehaviour
{
    public GameObject ShopUI;
    public GameObject FadeScreen;

    public GameObject shopSlotPrefab;
    public Transform shopSlotParent;
    public List<InventoryItemData> allAvailableItems;
    public List<ShopItem> itemsForSale;  

    public int shopLevel = 1;
    public GameObject FadeScrren;

    private void Start()
    {
        ShopUI.gameObject.SetActive(false);
        RollNewItems();
        PopulateShop();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
        }
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

    private int GetNumberOfItemsForSale()
    {
        return (4 + shopLevel) > allAvailableItems.Count ? (4 + shopLevel) : allAvailableItems.Count;
    }

    public void RollNewItems()
    {
        itemsForSale.Clear();

        int numberOfItems = GetNumberOfItemsForSale();

        for (int i = 0; i < numberOfItems; i++)
        {
            InventoryItemData randomItem = allAvailableItems[Random.Range(0, allAvailableItems.Count)];
            int randomQuantity = Random.Range(1, 21); 

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
            FadeScrren.gameObject.SetActive(false);
        }
        else
        {
            ShopUI.SetActive(true);
            GlobalVariables.CanAction = false;
            FadeScrren.gameObject.SetActive(true);
        }
    }

    public void OnRollButtonPressed()
    {
        RollNewItems();
    }

    public void OnButtonUpgradePressed()
    {
        shopLevel += 1;
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

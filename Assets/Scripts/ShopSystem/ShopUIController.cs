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
    public List<InventoryItemData> itemsForSale; 

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

        foreach (InventoryItemData itemData in itemsForSale)
        {
            GameObject slot = Instantiate(shopSlotPrefab, shopSlotParent);
            ShopSlotUI shopSlotUI = slot.GetComponent<ShopSlotUI>();
            shopSlotUI.InventoryItemData = itemData;
            shopSlotUI.SetUi();
        }
    }

    private int GetNumberOfItemsForSale()
    {
        return shopLevel;
    }

    public void RollNewItems()
    {
        itemsForSale.Clear();

        int numberOfItems = GetNumberOfItemsForSale();

        while (itemsForSale.Count < numberOfItems)
        {
            InventoryItemData randomItem = allAvailableItems[Random.Range(0, allAvailableItems.Count)];
            if (!itemsForSale.Contains(randomItem))
            {
                itemsForSale.Add(randomItem);
            }
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

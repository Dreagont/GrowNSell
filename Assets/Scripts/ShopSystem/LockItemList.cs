using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockItemList : MonoBehaviour
{
    public List<LockItemData> LockItems = new List<LockItemData>();
    public List<LockItemData> CraftingItems = new List<LockItemData>();
    public Transform LockItemParent;
    public Transform CraftingItemParent;
    public GameObject shopSlotPrefab;
    public GameObject craftingItemPrefab;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        PopulateShopUnlock();
        PopulateCraftingPanel();
    }

    void Update()
    {
        
    }
    public void PopulateCraftingPanel()
    {
        foreach (Transform child in CraftingItemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (LockItemData CraftingItem in CraftingItems)
        {
            GameObject slot = Instantiate(craftingItemPrefab, CraftingItemParent);
            LockItem lockItem = slot.GetComponent<LockItem>();
            LockItemUI lockItemUI = slot.GetComponent<LockItemUI>();

            lockItemUI.ItemIcon.sprite = CraftingItem.Icon;
            lockItem.LockItemData = CraftingItem;
        }
    }
    private void PopulateShopUnlock()
    {
        foreach (Transform child in LockItemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (LockItemData lockItemdata in LockItems)
        {
            GameObject slot = Instantiate(shopSlotPrefab, LockItemParent);
            LockItem lockItem = slot.GetComponent<LockItem>();
            LockItemUI lockItemUI = slot.GetComponent<LockItemUI>();

            if (lockItem.LockItemData.triggerIndex == 1)
            {
                lockItemUI.SetPrice(gameManager.ShopUpgradeCost);
            } else
            {
                lockItemUI.ItemPrice.text = lockItemdata.Price.ToString();
            }

            lockItemUI.ItemIcon.sprite = lockItemdata.Icon;
            lockItem.LockItemData = lockItemdata;
        }
    }
}

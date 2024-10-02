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
    void Start()
    {
        PopulateShopUnlock();
        PopulateCraftingPanel();
    }

    void Update()
    {
        
    }
    private void PopulateCraftingPanel()
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

            lockItemUI.ItemIcon.sprite = lockItemdata.Icon;
            lockItemUI.ItemPrice.text = lockItemdata.Price.ToString();
            lockItem.LockItemData = lockItemdata;
        }
    }
}

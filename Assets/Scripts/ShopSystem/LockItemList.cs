using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockItemList : MonoBehaviour
{
    public List<LockItemData> LockItems = new List<LockItemData>();
    public Transform LockItemParent;
    public GameObject shopSlotPrefab;

    void Start()
    {
        PopulateShopUnlock();
    }

    void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropItemData : MonoBehaviour
{
    public InventoryItemData inventoryItemData;
    public SpriteRenderer DropItemIcon;
    public TextMeshProUGUI DropItemCountText;
    public bool CanBePickedUp = false;
    public int dropCount;
    public bool isDouble = false;

    public void InitializeDrop(InventoryItemData itemData, int count)
    {
        this.inventoryItemData = itemData;
        this.dropCount = count;
    }
    public void InitializeDrop(InventoryItemData itemData, bool isDoubleDrop)
    {
        this.inventoryItemData = itemData;
        if (isDoubleDrop)
        {
            isDouble = true ;
            Debug.Log("yes");

        }
        else
        {
            isDouble = false ;
            Debug.Log("No");
        }
    }
    void Start()
    {
        DropItemIcon.sprite = inventoryItemData.dropIcon;
    }

    void Update()
    {
        if (inventoryItemData.SeedData != null) {
            this.dropCount = isDouble ? inventoryItemData.SeedData.DropCount * 2 : inventoryItemData.SeedData.DropCount;
        }
        DropItemCountText.text = dropCount.ToString();

    }
}

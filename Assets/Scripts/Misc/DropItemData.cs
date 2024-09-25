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

    public void InitializeDrop(InventoryItemData itemData)
    {
        this.inventoryItemData = itemData;
    }
    public void InitializeDrop(InventoryItemData itemData, bool isDoubleDrop)
    {
        this.inventoryItemData = itemData;
        if (isDoubleDrop)
        {
            isDouble = true ;
        } else
        {
            isDouble = false ;
        }
    }
    void Start()
    {
        DropItemIcon.sprite = inventoryItemData.dropIcon;
    }

    void Update()
    {
        this.dropCount = isDouble ? inventoryItemData.DropCount * 2 : inventoryItemData.DropCount;
        DropItemCountText.text = dropCount.ToString();

    }
}

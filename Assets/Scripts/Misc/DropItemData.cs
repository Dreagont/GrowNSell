using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropItemData : MonoBehaviour
{
    public InventoryItemData inventoryItemData;
    public int DropCount;
    public SpriteRenderer DropItemIcon;
    public TextMeshProUGUI DropItemCountText;
    public bool CanBePickedUp = false;


    public void InitializeDrop(InventoryItemData itemData, int dropCount)
    {
        this.inventoryItemData = itemData;
        this.DropCount = dropCount;
    }
    void Start()
    {
        DropItemIcon.sprite = inventoryItemData.dropIcon;
        DropItemCountText.text = DropCount.ToString();
    }

    void Update()
    {
        
    }
}

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


    public void InitializeDrop(InventoryItemData itemData)
    {
        this.inventoryItemData = itemData;
    }
    void Start()
    {
        DropItemIcon.sprite = inventoryItemData.dropIcon;
        DropItemCountText.text = inventoryItemData.GetTotalDropCount().ToString();
    }

    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.WSA;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] private int secondayInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public Image chestImage;

    public InventorySlotUi[] SlotUi;

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadAll;
        secondaryInventorySystem = new InventorySystem(secondayInventorySize);
        
       /* if (SaveGameManager.data == null)
        {
            SaveGameManager.data = new SaveData();
        }
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
        SaveGameManager.data.playerEquipment = new InventorySaveData(secondaryInventorySystem);
        SaveGameManager.data.playerAutoSell = new InventorySaveData(sellInventorySystem);
        SaveGameManager.data.playerAutoUse = new InventorySaveData(useInventorySystem); */

    }
    private void Start()
    {
        if (chestImage != null)
        {
            chestImage.GetComponent<Button>().onClick.AddListener(OpenChest);
        }
    }

    void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            OpenChest();
        }
    }


    public void LoadAll(SaveData data)
    {
        LoadInventory(data);
    }

    private void LoadInventory(SaveData data)
    {
        if (data != null && data.playerInventory.InventorySystem != null && data.playerEquipment.InventorySystem != null)
        {

            this.primaryInventorySystem = data.playerInventory.InventorySystem;
            this.secondaryInventorySystem = data.playerEquipment.InventorySystem;
            //this.sellInventorySystem = data.playerAutoSell.InventorySystem;
            //this.useInventorySystem = data.playerAutoUse.InventorySystem;
        }
    }
    private void ClearInventory(InventorySystem inventory)
    {
        foreach (var slot in inventory.InventorySlots)
        {
            if (slot.ItemData != null)
            {
                slot.ClearSlot();
            }
        }

        foreach (var slot in SlotUi)
        {
            slot?.ClearSlot();
        }

    }

    public void OpenChest()
    {
        onDynamicInventoryDisplayRequested?.Invoke(SecondaryInventorySystem);
    }

    public bool AddToInventory(InventoryItemData inventoryItemData, int amount)
    {
        if (secondaryInventorySystem.AddToInventory(inventoryItemData, amount))
        {
            return true;
        }

        return false;
    }
}

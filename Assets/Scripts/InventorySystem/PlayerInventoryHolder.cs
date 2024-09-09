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
    public int SelectedSlot = 0;
    public GameObject InventoryUiPanel;
    public InventoryItemData holdingItem;
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
            chestImage.GetComponent<Button>().onClick.AddListener(OpenInventory);
        }
    }

    void Update()
    {
        HandleSlotSelection();

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            OpenInventory();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseInventory();
        }

        holdingItem = GetHoldingItem();
    }
    private void HandleSlotSelection()
    {
        float scrollDelta = Mouse.current.scroll.ReadValue().y;
        if (scrollDelta > 0)
        {
            IncreaseSelectedSlot();
        }
        else if (scrollDelta < 0)
        {
            DecreaseSelectedSlot();
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectedSlot = 0;
        else if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectedSlot = 1;
        else if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectedSlot = 2;
        else if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectedSlot = 3;
        else if (Keyboard.current.digit5Key.wasPressedThisFrame) SelectedSlot = 4;
        else if (Keyboard.current.digit6Key.wasPressedThisFrame) SelectedSlot = 5;
        else if (Keyboard.current.digit7Key.wasPressedThisFrame) SelectedSlot = 6;
        else if (Keyboard.current.digit8Key.wasPressedThisFrame) SelectedSlot = 7;
        else if (Keyboard.current.digit9Key.wasPressedThisFrame) SelectedSlot = 8;
        else if (Keyboard.current.digit0Key.wasPressedThisFrame) SelectedSlot = 9;
    }

    private void IncreaseSelectedSlot()
    {
        SelectedSlot = (SelectedSlot + 1);
        if (SelectedSlot > 9)
        {
            SelectedSlot = 9;
        }
    }

    private void DecreaseSelectedSlot()
    {
        SelectedSlot = (SelectedSlot - 1);
        if (SelectedSlot < 0)
        {
            SelectedSlot = 0;
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

    public void OpenInventory()
    {
        if (InventoryUiPanel.gameObject.activeInHierarchy)
        {
            CloseInventory();
        } else
        {
            onDynamicInventoryDisplayRequested?.Invoke(SecondaryInventorySystem);
        }
    }
    public void CloseInventory()
    {
        InventoryUiPanel.gameObject.SetActive(false);
        GlobalVariables.CanUseInteractTools = true;

    }
    public bool AddToInventory(InventoryItemData inventoryItemData, int amount)
    {
        if (secondaryInventorySystem.AddToInventory(inventoryItemData, amount))
        {
            return true;
        }

        return false;
    }

    public bool AddToHotBar(InventoryItemData inventoryItemData, int amount)
    {
        if (primaryInventorySystem.AddToInventory(inventoryItemData, amount))
        {
            return true;
        } else
        if (secondaryInventorySystem.AddToInventory(inventoryItemData, amount))
        {
            return true;
        }

        return false;
    }

    public InventoryItemData GetHoldingItem()
    {
        return primaryInventorySystem.InventorySlots[SelectedSlot].ItemData;
    }
}

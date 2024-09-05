using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlotUi slotPrefab;

    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        ClearSlots();

        slotDictionary = new Dictionary<InventorySlotUi, InventorySlots>();

        if (inventoryToDisplay == null) return;

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventoryToDisplay.InventorySlots[i]);
            uiSlot.Init(inventoryToDisplay.InventorySlots[i]);
            uiSlot.UpdateInventorySlot();
        }

        inventorySystem = inventoryToDisplay; 
    }

    protected override void Start()
    {
        base.Start();
    }

    private void ClearSlots()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }

    public void RefreshDynamicInventory(InventorySystem inventoryToDisplay)
    {
        ClearSlot();
        inventorySystem = inventoryToDisplay;
        if (inventorySystem != null)
        {
           inventorySystem.OnInventorySlotsChanged += UpdateSlot;
        }
        AssignSlot(inventoryToDisplay);
    }

    private void ClearSlot()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }

    public void AddSlots(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            var newSlot = new InventorySlots(); // Create a new empty slot
            inventorySystem.AddSlot(newSlot);   // Add it to the inventory system
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, newSlot);
            uiSlot.Init(newSlot);
            uiSlot.UpdateInventorySlot();
        }
    }

    private void OnDisable()
    {
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotsChanged -= UpdateSlot;
        }
    }
}

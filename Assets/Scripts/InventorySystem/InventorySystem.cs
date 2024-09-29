using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlots> inventorySlots;
    public List<InventorySlots> InventorySlots => inventorySlots;

    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlots> OnInventorySlotsChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlots>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlots());
        }
    }

    public bool AddToInventory(InventoryItemData item, int amount)
    {
        if (ContainsItem(item, out List<InventorySlots> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amount))
                {
                    slot.AddToStack(amount);
                    OnInventorySlotsChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlots freeSlot))
        {
            freeSlot.UpdateInventorySlot(item, amount);
            OnInventorySlotsChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public int HaveItemInventory(InventoryItemData itemData)
    {
        int quantity = 0;
        foreach (var slot in inventorySlots)
        {
            if (slot.ItemData == itemData)
            {
                quantity += slot.StackSize;
            }
        }

        return quantity;
    }

    public int TakeItemFromInventory(InventoryItemData itemData, int amount)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.ItemData == itemData)
            {
                if (slot.StackSize >= amount)
                {
                    slot.RemoveFromStack(amount);
                    OnInventorySlotsChanged?.Invoke(slot);
                    return 0;
                }
                else
                {
                    amount -= slot.StackSize; 
                    slot.RemoveFromStack(slot.StackSize);
                    OnInventorySlotsChanged?.Invoke(slot);
                }
            }
        }

        return amount; 
    }


    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlots> invSlot)
    {
        invSlot = inventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot.Count > 0;
    }

    public bool HasFreeSlot(out InventorySlots freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot != null;
    }

    public void AddSlot(InventorySlots newSlot)
    {
        InventorySlots.Add(newSlot);
    }
}
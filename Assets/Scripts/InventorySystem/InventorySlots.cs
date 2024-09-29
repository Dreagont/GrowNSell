using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlots : ISerializationCallbackReceiver
{
    [NonSerialized] private InventoryItemData itemData;
    [SerializeField] private int itemID = -1;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlots(InventoryItemData source, int amount)
    {
        this.itemData = source;
        this.itemID = source.ID;
        this.stackSize = amount;
    }

    public InventorySlots()
    {
        ClearSlot();
    }

    public void AssignItem(InventorySlots slot)
    {
        if (itemData == slot.itemData)
        {
            AddToStack(slot.stackSize);
        }
        else
        {
            itemData = slot.itemData;
            itemID = itemData != null ? itemData.ID : -1;
            stackSize = 0;
            AddToStack(slot.StackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData item, int amount)
    {
        itemData = item;
        itemID = item != null ? item.ID : -1;
        stackSize = amount;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        if (stackSize >= amount)
        {
            stackSize -= amount;
        } else
        {
            stackSize -= stackSize;
        }
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = 0;
        itemID = -1;
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        return itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = itemData != null ? itemData.MaxStackSize - stackSize : 0;
        return RoomLeftInStack(amountToAdd);
    }

    public bool SplitHalfStack(out InventorySlots splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }
        int half = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(half);

        splitStack = new InventorySlots(ItemData, half);
        return true;
    }

    public bool SplitfStack(out InventorySlots splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }
        
        RemoveFromStack(1);

        splitStack = new InventorySlots(ItemData, 1);
        return true;
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (itemID == -1)
        {
            return;
        }
        var db = Resources.Load<Database>("Database");
        itemData = db.GetItem(itemID);
    }
}

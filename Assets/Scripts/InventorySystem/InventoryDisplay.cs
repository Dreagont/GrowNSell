using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlotUi, InventorySlots> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    protected Dictionary<InventorySlotUi, InventorySlots> SlotDictionary => slotDictionary;

    public abstract void AssignSlot(InventorySystem inventoryToDisplay);

    protected virtual void UpdateSlot(InventorySlots updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateInventorySlot(updatedSlot);
            }
        }
    }

    public virtual void UpdateSlotStatic(InventorySlots updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateInventorySlot(updatedSlot);
            }
        }
    }

    protected virtual void Start()
    {
    }

    public void SlotClicked(InventorySlotUi slot)
    {

        if (slot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AsssignedInventorySlot == null)
        {
            InventorySlots tempSlot = new InventorySlots(slot.AssignedInventorySlot.ItemData, slot.AssignedInventorySlot.StackSize);
            mouseInventoryItem.UpdateMouseSlot(tempSlot);
            slot.ClearSlot();
        }
        else if (slot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AsssignedInventorySlot != null)
        {
            if (slot.equipableTag == mouseInventoryItem.AsssignedInventorySlot.ItemData.EquipableTag || slot.equipableTag == EquipableTag.None)
            {
                slot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AsssignedInventorySlot);
                slot.UpdateInventorySlot();
                mouseInventoryItem.ClearSlot();
            }


        }
        else if (slot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AsssignedInventorySlot != null)
        {
            bool isSameItem = slot.AssignedInventorySlot.ItemData == mouseInventoryItem.AsssignedInventorySlot.ItemData;
            if (isSameItem && slot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AsssignedInventorySlot.StackSize))
            {
                slot.AssignedInventorySlot.AddToStack(mouseInventoryItem.AsssignedInventorySlot.StackSize);
                slot.UpdateInventorySlot();
                mouseInventoryItem.ClearSlot();

            }
            else if (isSameItem && !slot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AsssignedInventorySlot.StackSize, out int leftInStack))
            {
                if (leftInStack < 1)
                {
                    SwapSlot(slot);
                }
                else
                {
                    int remainingOnMouse = mouseInventoryItem.AsssignedInventorySlot.StackSize - leftInStack;
                    slot.AssignedInventorySlot.AddToStack(leftInStack);
                    slot.UpdateInventorySlot();

                    var newItem = new InventorySlots(mouseInventoryItem.AsssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }
            else if (!isSameItem)
            {
                SwapSlot(slot);
            }
        }
    }

    private void SwapSlot(InventorySlotUi slot)
    {
        InventorySlots tempSlot = new InventorySlots(slot.AssignedInventorySlot.ItemData, slot.AssignedInventorySlot.StackSize);

        slot.ClearSlot();
        slot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AsssignedInventorySlot);
        slot.UpdateInventorySlot();

        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(tempSlot);
    }

}
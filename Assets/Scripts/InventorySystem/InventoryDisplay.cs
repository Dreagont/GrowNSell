using System.Collections.Generic;
using System.Linq;
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
        mouseInventoryItem = FindAnyObjectByType<MouseItemData>();
    }

    public void SlotClicked(InventorySlotUi slot)
    {
        if (slot == null)
        {
            Debug.LogWarning("Asdas");

            return;
        }


        if (slot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AsssignedInventorySlot == null)
        {
            InventorySlots tempSlot = new InventorySlots(slot.AssignedInventorySlot.ItemData, slot.AssignedInventorySlot.StackSize);
            mouseInventoryItem.UpdateMouseSlot(tempSlot);
            slot.ClearSlot();
        }
        else if (slot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AsssignedInventorySlot != null)
        {
                slot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AsssignedInventorySlot);
                slot.UpdateInventorySlot();
                mouseInventoryItem.ClearSlot();

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

    public void SplitStack(InventorySlotUi slot)
    {
        /*if (mouseInventoryItem.AsssignedInventorySlot.ItemData == slot.AssignedInventorySlot.ItemData)
        {
            
            if ( slot.AssignedInventorySlot.SplitfStack(out InventorySlots item))
            {
                mouseInventoryItem.UpdateStack();
                slot.UpdateInventorySlot();

                Debug.Log("The same");
            }
            
        } else */
        if (mouseInventoryItem.AsssignedInventorySlot == null)
        {
            Debug.Log("slot null");
            if (slot.AssignedInventorySlot.SplitfStack(out InventorySlots item))
            {
                mouseInventoryItem.UpdateMouseSlot(item);
                slot.UpdateInventorySlot();
            }

        } else
        {
            if (slot.AssignedInventorySlot.SplitfStack(out InventorySlots item))
            {
                var temp = mouseInventoryItem.AsssignedInventorySlot;
                mouseInventoryItem.UpdateMouseSlot(temp);
                mouseInventoryItem.AsssignedInventorySlot.AddToStack(1);
                mouseInventoryItem.ItemCount.text = mouseInventoryItem.AsssignedInventorySlot.StackSize.ToString();
                slot.UpdateInventorySlot();
            }

        }

    }
    private void ReturnItemToOriginalSlot()
    {
        if (mouseInventoryItem.AsssignedInventorySlot != null)
        {
            var originalSlot = slotDictionary.FirstOrDefault(x => x.Value == mouseInventoryItem.AsssignedInventorySlot).Key;

            if (originalSlot != null)
            {
                originalSlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AsssignedInventorySlot);
                originalSlot.UpdateInventorySlot();
                mouseInventoryItem.ClearSlot();
            }
            else
            {
                Debug.LogWarning("Could not find the original slot for the item. The item will be lost.");
                mouseInventoryItem.ClearSlot();
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
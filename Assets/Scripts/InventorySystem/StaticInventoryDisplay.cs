using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlotUi[] slots;
    private PlayerInventoryHolder playerInventoryHolder;
    public override void AssignSlot(InventorySystem inventoryToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlotUi, InventorySlots>();

        if (slots.Length != inventoryToDisplay.InventorySize)
        {
            Debug.LogWarning($"out of sync on {this.gameObject}");
        }

        for (int i = 0; i < inventoryToDisplay.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventoryToDisplay.InventorySlots[i]);
            slots[i].Init(inventoryToDisplay.InventorySlots[i]);
        }
    }

    protected override void Start()
    {
        playerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        slots[0].IsSelected.gameObject.SetActive(true);
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            InventorySystem.OnInventorySlotsChanged += UpdateSlotStatic;
            AssignSlot(inventorySystem);
        }
        else
        {
            Debug.LogWarning("no inventoryHolder assigned to StaticInventoryDisplay");
        }
    }


    private void Update()
    {
        
       HighLight(playerInventoryHolder.SelectedSlot);
    }

    public void HighLight(int index)
    {
       for (int i = 0;i < slots.Length;i++)
       {
            if (index != i)
            {
                slots[i].IsSelected.gameObject.SetActive(false);
            }
       }
        slots[index].IsSelected.gameObject.SetActive(true);
    }
}



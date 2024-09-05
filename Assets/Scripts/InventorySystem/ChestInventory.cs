using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChestInventory : MonoBehaviour
{
    public GameObject InventoryUI;
    public DynamicInventoryDisplay inventoryPanel;
    public InventorySystem chestInventory;
    public Image chestImage;
    public Button addSlotButton;
    private void Start()
    {

        addSlotButton.onClick.AddListener(() => AddSlotsToInventory(0));

        if (chestImage != null)
        {
            chestImage.GetComponent<Button>().onClick.AddListener(OpenChest);
        }

    }

    private void Update()
    {
       
    }

    private void OpenChest()
    {
        if (InventoryUI.gameObject.activeInHierarchy)
        {
            InventoryUI.gameObject.SetActive(false);
        } else {
            InventoryUI.gameObject.SetActive(true);
        }
    }

    private void AddSlotsToInventory(int quantity)
    {
        inventoryPanel.AddSlots(quantity);
    }
}

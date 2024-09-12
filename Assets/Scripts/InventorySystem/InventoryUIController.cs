using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public GameObject inventoryPanelOuter;

    
    private void Awake()
    {
        inventoryPanelOuter.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.onDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.onDynamicInventoryDisplayRequested -= DisplayInventory;

    }

    void Update()
    {

    }

    void DisplayInventory (InventorySystem inventoryToDisplay)
    {
        inventoryPanelOuter.gameObject.SetActive(true);
        GlobalVariables.CanAction = false;
        inventoryPanel.RefreshDynamicInventory(inventoryToDisplay);

    }
}

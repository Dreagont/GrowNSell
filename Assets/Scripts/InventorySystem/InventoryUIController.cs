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

        if (inventoryPanel.gameObject.activeInHierarchy)
        { 
            if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.xKey.wasPressedThisFrame)
            {
                CloseInventory();
            }
        } 

       

    }

    public void CloseInventory()
    {
        inventoryPanelOuter.gameObject.SetActive(false);
    }

    private void OpenChest()
    {
        if (inventoryPanelOuter.gameObject.activeInHierarchy)
        {
            inventoryPanelOuter.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanelOuter.gameObject.SetActive(true);
        }
    }

    void DisplayInventory (InventorySystem inventoryToDisplay)
    {
        inventoryPanelOuter.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(inventoryToDisplay);

    }
}

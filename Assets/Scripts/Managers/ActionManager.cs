using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public PlayerInventoryHolder InventoryHolder;
    public InventoryItemData itemData1;
    public InventoryItemData itemData2;

    void Start()
    {
        
    }

    void Update()
    {
        Pickup();
    }

    public void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            InventoryHolder.AddToHotBar(itemData1, 10);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            InventoryHolder.AddToHotBar(itemData2, 1);
        }
    }



}

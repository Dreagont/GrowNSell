using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public PlayerInventoryHolder InventoryHolder;
    public InventoryItemData Hoe;
    public InventoryItemData Axe;
    public InventoryItemData Pickaxe;
    public InventoryItemData WateringCan;
    public InventoryItemData Shovel;

    void Start()
    {
        Pickup();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            

        }
    }

    public void Pickup()
    {
        InventoryHolder.AddToHotBar(Hoe, 1);
        InventoryHolder.AddToHotBar(Axe, 1);
        InventoryHolder.AddToHotBar(Pickaxe, 1);
        InventoryHolder.AddToHotBar(WateringCan, 1);
        //InventoryHolder.AddToHotBar(Shovel, 1);
    }



}

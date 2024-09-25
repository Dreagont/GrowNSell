using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    public InventoryItemData[] PlowItemDrops;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InventoryItemData DropPlowItem(float chance)
    {
        float isDrop = Random.Range(0f, 1f); 
        if (isDrop <= chance && PlowItemDrops.Length > 0) 
        {
            int randomIndex = Random.Range(0, PlowItemDrops.Length); 
            return PlowItemDrops[randomIndex];
            
            
        }
        else
        {
           return null;
        }
    }
}

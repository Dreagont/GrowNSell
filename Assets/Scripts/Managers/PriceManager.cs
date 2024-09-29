using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceManager : MonoBehaviour
{
    public InventoryItemData[] inventoryItemDataArray;

    void Start()
    {
    }

    private void Update()
    {
        
    }

    public void RollAllSellPrices()
    {
        foreach (var itemData in inventoryItemDataArray)
        {
            float roll = Random.value;
            roll = Mathf.Pow(roll, 2);
            int rolledPrice = Mathf.RoundToInt(Mathf.Lerp(itemData.minSellPrice, itemData.maxSellPrice, roll));
            itemData.sellPrice = rolledPrice;
        }
    }
}

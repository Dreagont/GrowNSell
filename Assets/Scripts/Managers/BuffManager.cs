using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public InventoryItemData[] itemCanApplyBuff;

    public InventoryItemData itemData;
    public Buff Buff;
    void Start()
    {
        ResetAllBuff();
        ApplyBuff(itemData,Buff);
    }

    void Update()
    {
        
    }

    public void ApplyBuff(InventoryItemData item, Buff buff)
    {
        item.AddBuff(buff);
    }

    public void ResetAllBuff()
    {
        foreach (var item in itemCanApplyBuff)
        {
            item.ResetBuff();
        }
    }
}

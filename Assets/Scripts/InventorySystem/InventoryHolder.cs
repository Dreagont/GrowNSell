using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

   /* [SerializeField] private int sellInventorySize;
    [SerializeField] protected InventorySystem sellInventorySystem;
    public InventorySystem SellInventorySystem => sellInventorySystem;

    [SerializeField] private int useInventorySize;
    [SerializeField] protected InventorySystem useInventorySystem;
    public InventorySystem UseInventorySystem => useInventorySystem;
    */
    public static UnityAction<InventorySystem> onDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        primaryInventorySystem = new InventorySystem(inventorySize);
        //sellInventorySystem = new InventorySystem(sellInventorySize);
        //useInventorySystem = new InventorySystem(useInventorySize);
    }
}
[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InventorySystem;

    public InventorySaveData(InventorySystem inventorySystem)
    {
        this.InventorySystem = inventorySystem;

    }
}
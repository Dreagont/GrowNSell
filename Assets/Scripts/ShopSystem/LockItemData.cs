using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Crafting System/Lock Item")]
public class LockItemData : ScriptableObject
{
    public CraftingMaterial[] craftingMaterials;
    public string ItemName;
    public string ItemDescription;
    public int Price;
    public Sprite Icon;
    public InventoryItemData[] lockItem;
    public int triggerIndex = -1;
    public LockItemData[] lockItemDatas;
}

[System.Serializable]
public struct CraftingMaterial
{
    public InventoryItemData InventoryItemData;
    public int Quantity;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID = -1;
    public string displayName;
    public string description;
    public int MaxStackSize;

    public Sprite icon;
    public Sprite dropIcon;

    public int minSellPrice = 10;
    public int sellPrice = 15;
    public int maxSellPrice = 20;
    public int buyPrice = 15;

    public ItemType itemType1;
    public ItemType itemType2;

    public EquipableTag EquipableTag;

    public SeedData SeedData;
    public PlaceAbleObjectData PlaceAbleObjectData;
    public ToolData ToolData;
    public GameObject Prefab;

    private int originalMinSellPrice;
    private int originalMaxSellPrice;

    void OnEnable()
    {
        originalMinSellPrice = minSellPrice;
        originalMaxSellPrice = maxSellPrice;
    }

    public void ResetPrices()
    {
        minSellPrice = originalMinSellPrice;
        maxSellPrice = originalMaxSellPrice;
    }
}

public enum ItemType
{
    None,
    Interaction,
    Seed,
    Crop,
    PlaceAble,
    Material
}

public enum EquipableTag
{
    None,
    Hoe,
    WateringCan,
    Shovel,
    Axe,
    Pickaxe,
    FishingRod,
    Hammer
}

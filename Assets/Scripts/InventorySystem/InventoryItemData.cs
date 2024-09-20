using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID = -1;
    public string displayName;
    public string description;
    public Sprite icon;
    public Sprite dropIcon;
    public int MaxStackSize;
    public int sellPrice = 10;
    public int buyPrice = 15;

    public ItemType itemType1;
    public ItemType itemType2;

    public EquipableTag EquipableTag;
}
public enum ItemType
{
    None,
    Interaction,
    Seed,
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

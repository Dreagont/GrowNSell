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
    public int MaxStackSize;
    public int sellPrice = 10;

    public ItemType itemType1;
    public ItemType itemType2;

    public ConsumeStats consumeStats;

    public EquipableTag EquipableTag;

    public int bonusHealth;
    public int bonusArmor;
    public int bonusDamage;
    public int bonusAttackSpeed;
    public int bonusRegen;
}
public enum ItemType
{
    None,
    Equipable,
    Consumable,
    Marterial
}

public enum EquipableTag
{
    None,
    Chest,
    Legs,
    Weapon,
    Helmet,
    Boots
}

[System.Serializable]
public class ConsumeStats
{
    public int GoldGain;
    public int EnergyGain;
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
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
    public int DropCount = 1;
    public int Experient = 5;
    public ItemType itemType1;
    public ItemType itemType2;

    public EquipableTag EquipableTag;
    public GameObject Prefab;

    public List<Buff> buffs = new List<Buff>();

    public float GetTotalSellPrice()
    {
        float totalPercentageIncrease = 0f;
        foreach (var buff in buffs)
        {
            totalPercentageIncrease += buff.sellPriceMultiplier;
        }
        return sellPrice * (1 + totalPercentageIncrease);
    }

    public int GetTotalDropCount()
    {
        float totalPercentageIncrease = 0f;
        foreach (var buff in buffs)
        {
            totalPercentageIncrease += buff.dropCountMultiplier;
        }
        return (int)(DropCount * (1 + totalPercentageIncrease));
    }

    public float GetTotalExperience()
    {
        float totalPercentageIncrease = 0f;
        foreach (var buff in buffs)
        {
            totalPercentageIncrease += buff.expMultiplier;
        }
        return (int)(Experient * (1 + totalPercentageIncrease));
    }

    public void AddBuff(Buff newBuff)
    {
        buffs.Add(newBuff);
    }

    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
    }

    public void ResetBuff()
    {
        buffs.Clear();
    }
}

public enum ItemType
{
    None,
    Interaction,
    Seed,
    Crop
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

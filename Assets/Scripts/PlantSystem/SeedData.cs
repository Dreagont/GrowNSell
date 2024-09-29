using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planting System/Seed")]

public class SeedData : ScriptableObject
{
    public string SeedName;
    public int GrowTime;
    public int Experient = 5;
    public int DropCount = 5;

    public float DoubleExp = 0;
    public float DoubleDrop = 0;
    public float NotDestroy = 0;

    public string Description;

    public InventoryItemData SeedProduct;
}

public enum Season
{
    None,
    Spring,
    Summer,
    Autum,
    Winter
}

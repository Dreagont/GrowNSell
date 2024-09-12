using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planting System/Seed")]

public class SeedData : ScriptableObject
{
    public string SeedName;
    public int SeedLevel;
    public int GrowTime;
    public Season Season;
}

public enum Season
{
    None,
    Spring,
    Summer,
    Autum,
    Winter
}

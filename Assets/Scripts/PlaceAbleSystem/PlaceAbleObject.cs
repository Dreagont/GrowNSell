using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlaceAble System/PlaceAble")]
public class PlaceAbleObject : ScriptableObject
{
    public int Radius;
    public PlaceAbleType PlaceAbleType;
    public int Health;
}

public enum PlaceAbleType
{
    None,
    Sprinkler,
    TreeGen,
    MineralGen
}

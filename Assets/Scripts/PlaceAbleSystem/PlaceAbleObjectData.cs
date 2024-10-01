using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlaceAble System/PlaceAble")]
public class PlaceAbleObjectData : ScriptableObject
{
    public int Radius;
    public PlaceAbleType PlaceAbleType;
    public int Health;
    public ObjectData PlaceItemObjectData;
    public List<GameObject> SpawnedObjects;
}

public enum PlaceAbleType
{
    None,
    Sprinkler,
    TreeGen,
    MineralGen
}

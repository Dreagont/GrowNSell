using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Object System/Object")]
public class ObjectData : ScriptableObject
{
    public string Name;
    public InventoryItemData[] DropItems;
    public int DropQuantity;
    public int Health = 10;
    public int Experience = 2;
    public ObjectType ObjectType;
}

public enum ObjectType
{
    Wood,
    Stone,
}

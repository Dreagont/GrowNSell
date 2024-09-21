using UnityEngine;

[CreateAssetMenu(menuName = "Buff System/Buff")]
public class Buff : ScriptableObject
{
    public InventoryItemData itemBuffed;
    public string buffName;
    public string description;
    public float sellPriceMultiplier = 0f;
    public float dropCountMultiplier = 0f;
    public float expMultiplier = 0f;
    public Sprite buffIcon;
}

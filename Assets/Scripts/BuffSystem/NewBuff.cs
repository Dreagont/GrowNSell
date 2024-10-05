using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buff System/New Buff")]
public class NewBuff : ScriptableObject
{
    public Sprite BuffIcon;
    public string buffName;
    public string description;
    public int MaxEnergyBuff = 0;
    public int EnergyReductionBuff = 0;
    public float ExperientBuff = 0;
    public float CropGoldBuff = 0;
    public float MaterialGoldBuff = 0;
    public int ToolDamageBuff = 0;
    public float PlowDropChance = 0;
    public float DoubleDropChance = 0f;
    public float NotDestroyFarmland = 0f;
    public int MaterialBonus = 0;
    public int MineRange = 0;
    public int TreeRegrow = 0;

}

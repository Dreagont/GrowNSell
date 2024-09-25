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
    public float GoldBuff = 0;
    public int ToolDamageBuff = 0;
    public float PlowDropChance = 0;
    public float DoubleDropChance = 0f;

}

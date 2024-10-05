using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    public GameManager GameManager;

    public TextMeshProUGUI CurrentGold;
    public TextMeshProUGUI TotalGold;
    public TextMeshProUGUI CropGold;
    public TextMeshProUGUI MaterialGold;
    public TextMeshProUGUI DoubleDropChance;
    public TextMeshProUGUI EnergyReduction;
    public TextMeshProUGUI ExpMulti;
    public TextMeshProUGUI MaterialDrop;
    public TextMeshProUGUI MaxEnergy;
    public TextMeshProUGUI TreePDay;
    public TextMeshProUGUI SeedDrop;
    public TextMeshProUGUI MineRange;
    public TextMeshProUGUI PlayerName;
    void Start()
    {
        SetUI();
    }

    void Update()
    {
        
    }

    public void SetUI()
    {
        CurrentGold.text = GlobalVariables.FormatNumber(GameManager.GoldManager.Gold);
        TotalGold.text = GlobalVariables.FormatNumber(GameManager.GoldManager.TotalGold);
        CropGold.text = (GameManager.BuffManager.CropGoldBuff * 100).ToString() + "%";
        MaterialGold.text = (GameManager.BuffManager.MaterialGoldBuff * 100).ToString() + "%";
        DoubleDropChance.text = (GameManager.BuffManager.DoubleDropChance * 100).ToString() + "%";
        EnergyReduction.text = (GameManager.BuffManager.EnergyReductionBuff).ToString();
        ExpMulti.text = (GameManager.BuffManager.ExperientBuff * 100).ToString() + "%";
        MaterialDrop.text = (GameManager.BuffManager.MaterialBonus).ToString();
        MaxEnergy.text = (GameManager.BuffManager.MaxEnergyBuff + 500).ToString();
        TreePDay.text = (GameManager.BuffManager.TreeRegrow).ToString();
        SeedDrop.text = (GameManager.BuffManager.PlowDropChance * 100).ToString() + "%";
        MineRange.text = (GameManager.BuffManager.MineRange).ToString();
        PlayerName.text = GameManager.PlayerName;
    }
}

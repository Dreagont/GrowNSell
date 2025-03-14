using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuffManager : MonoBehaviour
{
    public GameObject BuffShopUI;
    public GameObject FadeScreen;

    public int MaxEnergyBuff = 0;
    public int EnergyReductionBuff = 0;
    public float ExperientBuff = 0;
    public float CropGoldBuff = 0;
    public float MaterialGoldBuff = 0;
    public int ToolDamageBuff = 0;
    public float PlowDropChance = 0f;
    public float DoubleDropChance = 0f;
    public float NotDestroyFarmland = 0.2f;
    public int MaterialBonus = 0;
    public int MineRange = 0;
    public int TreeRegrow = 3;

    public List<NewBuff> newBuffs = new List<NewBuff>();

    public GameObject buffSlotPrefab;
    public Transform buffSlotParent;
    public List<NewBuff> allAvailableBuffs;
    public List<NewBuff> buffsForSale;
    public int numberOfBuffsToDisplay = 3;

    public GameManager GameManager;
    void Start()
    {
        RollNewBuffs();
        PopulateBuffShop();
        ResetAllBuff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyAllBuff()
    {
        MaxEnergyBuff = 0;
        EnergyReductionBuff = 0;
        ExperientBuff = 0;
        CropGoldBuff = 0;
        MaterialGoldBuff = 0;
        ToolDamageBuff = 0;
        PlowDropChance = 0;
        DoubleDropChance = 0;
        NotDestroyFarmland = 0.2f;
        MaterialBonus = 0;
        MineRange = 0;
        TreeRegrow = 3;
        foreach (var buff in newBuffs)
        {
            MaxEnergyBuff += buff.MaxEnergyBuff;
            EnergyReductionBuff += buff.EnergyReductionBuff;
            ExperientBuff += buff.ExperientBuff;
            CropGoldBuff += buff.CropGoldBuff;
            MaterialGoldBuff += buff.MaterialGoldBuff;
            ToolDamageBuff += buff.ToolDamageBuff;
            PlowDropChance += buff.PlowDropChance;
            DoubleDropChance += buff.DoubleDropChance;
            NotDestroyFarmland += buff.NotDestroyFarmland;
            MaterialBonus += buff.MaterialBonus;
            MineRange += buff.MineRange;
            TreeRegrow += buff.TreeRegrow;
            if (CropGoldBuff <= -1)
            {
                CropGoldBuff = 1;
            }
        }
        GameManager.EnergyManager.EnergyIncrease();
    }

    public void AddBuff(NewBuff buff)
    {
        newBuffs.Add(buff);
        ApplyAllBuff();
    }


    public void ResetAllBuff()
    {
        newBuffs.Clear();
    }

    public void RollNewBuffs()
    {
        buffsForSale.Clear();

        List<NewBuff> availableBuffs = allAvailableBuffs;

        for (int i = 0; i < numberOfBuffsToDisplay; i++)
        {
            if (availableBuffs.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableBuffs.Count);
            NewBuff randomBuff = availableBuffs[randomIndex];

            if (buffsForSale.Contains(randomBuff))
            {
                i--;
                continue;
            }

            buffsForSale.Add(randomBuff);
        }

        PopulateBuffShop();
    }

    private void PopulateBuffShop()
    {
        foreach (Transform child in buffSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (NewBuff buff in buffsForSale)
        {
            GameObject slot = Instantiate(buffSlotPrefab, buffSlotParent);
            NewBuffUI buffUI = slot.GetComponent<NewBuffUI>();
            buffUI.buff = buff;
            buffUI.UpdateBuffUI();
        }
    }

    public void OnRollButtonPressed()
    {
        RollNewBuffs();
    }

    public void ToggleBuffShopUI()
    {
        if (BuffShopUI.activeInHierarchy)
        {
            BuffShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
            FadeScreen.SetActive(false);
        }
        else
        {
            BuffShopUI.SetActive(true);
            GlobalVariables.CanAction = false;
            FadeScreen.SetActive(true);
            RollNewBuffs();
        }
    }
}

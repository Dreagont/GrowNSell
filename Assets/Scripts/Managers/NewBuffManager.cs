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
    public float GoldBuff = 0;
    public int ToolDamageBuff = 0;
    public float PlowDropChance = 0f;
    public float DoubleDropChance = 0f;
    public float NotDestroyFarmland = 0.2f;

    public List<NewBuff> newBuffs = new List<NewBuff>();

    public GameObject buffSlotPrefab;
    public Transform buffSlotParent;
    public List<GameObject> allAvailableBuffs;
    public List<GameObject> buffsForSale;
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
        GoldBuff = 0;
        ToolDamageBuff = 0;
        PlowDropChance = 0;
        DoubleDropChance = 0;
        NotDestroyFarmland = 0.2f;
        foreach (var buff in newBuffs)
        {
            MaxEnergyBuff += buff.MaxEnergyBuff;
            EnergyReductionBuff += buff.EnergyReductionBuff;
            ExperientBuff += buff.ExperientBuff;
            GoldBuff += buff.GoldBuff;
            ToolDamageBuff += buff.ToolDamageBuff;
            PlowDropChance += buff.PlowDropChance;
            DoubleDropChance += buff.DoubleDropChance;
            NotDestroyFarmland += buff.NotDestroyFarmland;

            if (GoldBuff <= -1)
            {
                GoldBuff = 1;
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

        List<GameObject> availableBuffs = new List<GameObject>(allAvailableBuffs);

        for (int i = 0; i < numberOfBuffsToDisplay; i++)
        {
            if (availableBuffs.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableBuffs.Count);
            GameObject randomBuff = availableBuffs[randomIndex];

            buffsForSale.Add(randomBuff);
            availableBuffs.RemoveAt(randomIndex);
        }

        PopulateBuffShop();
    }

    private void PopulateBuffShop()
    {
        foreach (Transform child in buffSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject buff in buffsForSale)
        {
            GameObject slot = Instantiate(buff, buffSlotParent);
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

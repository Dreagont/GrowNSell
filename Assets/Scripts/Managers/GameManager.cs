using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Gold = 1000;
    public int GoalGold = 5000;
    public int DayCheck = 5;

    public int Energy = 100;
    public int CurrentEnergy = 100;

    public ExperientManager ExperientManager;

    public int ShopRollCost = 10;
    public int ShopUpgradeCost = 1000;
    public Text GoldText;
    public Text GoalGoldText;
    public Text GoalGoldText1;

    public TextMeshProUGUI GoalDayText;
    public TextMeshProUGUI WinMessage;

    public GameObject WinGameWindow;
    public GameObject LoseGameWindow;
    public GameObject GamePenaty;

    public int MaxEnergyBuff = 0;
    public int EnergyReductionBuff = 0;
    public float ExperientBuff = 0;
    public float GoldBuff = 0;
    public int ToolDamageBuff = 0;
    public float PlowDropChance = 0f;
    public float DoubleDropChance = 0f;

    public DropsManager DropsManager;

    public List<NewBuff> newBuffs = new List<NewBuff>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextUI();
        CompleteGoal();

        //Energy += EnergyBuff;

        if (Gold >= GoalGold)
        {
            GoalGoldText.color = Color.green;
        } else
        {
            GoalGoldText.color = Color.red;
        }
    }

    public void UpdateTextUI()
    {
        if (Gold < 100000)
        {
            GoldText.text = Gold.ToString();
        }
        else
        {
            GoldText.text = GlobalVariables.FormatNumber(Gold);

        }
        if (GoalGold < 100000)
        {
            GoalGoldText.text = GoalGold.ToString();
            GoalGoldText1.text = GoalGold.ToString();
        }
        else
        {
            GoalGoldText.text = GlobalVariables.FormatNumber(GoalGold);
            GoalGoldText1.text = GlobalVariables.FormatNumber(GoalGold);

        }
        GoalDayText.text ="in Day " + (DayCheck + 2).ToString();
    }
    public bool CanAffordItem(int itemPrice)
    {
        return Gold - itemPrice >= 0;
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
        foreach (var buff in newBuffs)
        {
            MaxEnergyBuff += buff.MaxEnergyBuff;
            EnergyReductionBuff += buff.EnergyReductionBuff;
            ExperientBuff += buff.ExperientBuff;
            GoldBuff += buff.GoldBuff;
            ToolDamageBuff += buff.ToolDamageBuff;
            PlowDropChance += buff.PlowDropChance;
            DoubleDropChance += buff.DoubleDropChance;
        }
        EnergyIncrease();
    }

    public void AddBuff(NewBuff buff)
    {
        newBuffs.Add(buff);
        ApplyAllBuff();
    }

    public void EnergyIncrease()
    {
        Energy = 400 + MaxEnergyBuff;
        
    }

    public float GetGoldAmount(int amount)
    {
        return (amount * (1 + GoldBuff));
    }

    public void ConsumeEnergyTools(int energyCost,int energyMulti)
    {
        int outEnergy = energyCost - (EnergyReductionBuff * energyMulti);
        if (outEnergy >= 1)
        {
            CurrentEnergy = CurrentEnergy - outEnergy;

        }
        else
        {
            CurrentEnergy -= 1;
        }
    }

    public bool ActionAble(int energyCost)
    {
        return CurrentEnergy >= energyCost;
    }

    public void RefillEnergy()
    {
        CurrentEnergy = Energy;
    }

    public void CompleteGoal()
    {
        if (GlobalVariables.currentDay == DayCheck + 2)
        {
            GamePenaty.SetActive(true);
            if (Gold <= GoalGold)
            {
                LoseGame();
                
            } else
            {
                WinGame();
               
            }
        }

    }

    public bool isDoubleDrop(float chance)
    {
        float isDrop = Random.Range(0f, 1f);
        return isDrop <= chance;
    }

    private void LoseGame()
    {
        LoseGameWindow.SetActive(true);
        GlobalVariables.CanAction = false;
    }

    private void WinGame()
    {
        WinGameWindow.SetActive(true);
        DayCheck += 7;
        GoalGold *= 4;
        WinMessage.text = "Continue with " + GoalGold.ToString() + " Goal gold.";

    }

    public void RestartGame()
    {
        GamePenaty.SetActive(false);
        GlobalVariables.currentDay = 0;
        GlobalVariables.CanAction = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

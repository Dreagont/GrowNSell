using System;
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

    public int ShopRollCost = 10;
    public int ShopUpgradeCost = 1000;
    public Text GoldText;
    public Text GoalGoldText;
    public Text GoalGoldText1;

    public TextMeshProUGUI GoalDayText;

    public GameObject WinGameWindow;
    public GameObject LoseGameWindow;
    public GameObject GamePenaty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextUI();
        CompleteGoal();
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
        GoalDayText.text ="in Day " + DayCheck.ToString();
    }
    public bool CanAffordItem(int itemPrice)
    {
        return Gold - itemPrice >= 0;
    }


    public void ConsumeEnergy(int energyCost)
    {
        CurrentEnergy -= energyCost;
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
        if (GlobalVariables.currentDay == DayCheck + 1)
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

    private void LoseGame()
    {
        LoseGameWindow.SetActive(true);
        GlobalVariables.CanAction = false;
    }

    private void WinGame()
    {
        WinGameWindow.SetActive(true);
        DayCheck += 10;
        GoalGold *= 4;
    }

    public void RestartGame()
    {
        GamePenaty.SetActive(false);
        GlobalVariables.currentDay = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

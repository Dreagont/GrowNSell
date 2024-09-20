using System;
using System.Collections;
using System.Collections.Generic;
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

    public Text GoldText;
    public Text GoalGoldText;
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
        }
        else
        {
            GoalGoldText.text = GlobalVariables.FormatNumber(GoalGold);

        }
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
        if (GlobalVariables.currentDay == DayCheck )
        {
            if (Gold <= GoalGold)
            {
                GlobalVariables.currentDay = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else
            {
                
                DayCheck *= 2;
                GoalGold *= 4;
            }
        }

    }

}

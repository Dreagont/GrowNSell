using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Gold = 1000;

    public int Energy = 100;
    public int CurrentEnergy = 100;

    public Text GoldText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gold < 1000000000)
        {
            GoldText.text = Gold.ToString();
        } else
        {
            GoldText.text = GlobalVariables.FormatNumber(Gold);

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
}

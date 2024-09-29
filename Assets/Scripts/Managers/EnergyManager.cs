using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image Fillbar;
    public GameManager gameManager;
    public float review = 0;

    public int Energy = 100;
    public int CurrentEnergy = 100;

    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
        Fillbar.fillAmount =GlobalVariables.UpdateFillBar(CurrentEnergy, Energy);
    }

    // Update is called once per frame
    void Update()
    {
        Fillbar.fillAmount = GlobalVariables.UpdateFillBar(CurrentEnergy, Energy);
    }

    public void EnergyIncrease()
    {
        Energy = 400 + gameManager.BuffManager.MaxEnergyBuff;

    }
    public void ConsumeEnergyTools(int energyCost, int energyMulti)
    {
        int outEnergy = energyCost - (gameManager.BuffManager.EnergyReductionBuff * energyMulti);
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


}

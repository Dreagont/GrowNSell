using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GoldManager GoldManager;

    public ExperientManager ExperientManager;

    public EnergyManager EnergyManager;

    public DropsManager DropsManager;

    public NewBuffManager BuffManager;

    public int ShopRollCost = 10;
    public int ShopUpgradeCost = 1000;

    void Start()
    {
    }

    void Update()
    {
    }
   

    public float GetGoldAmount(int amount)
    {
        return (amount * (1 + BuffManager.GoldBuff));
    }

   

    public bool IsSuccess(float chance)
    {
        float isDrop = Random.Range(0f, 1f);
        return isDrop <= chance;
    }
}

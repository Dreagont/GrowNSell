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

    public string PlayerName;

    public int ShopRollCost = 10;
    public int ShopUpgradeCost = 1000;

    public bool canAction = true;

    void Start()
    {
        if (DontDestroyWhenReload.Instance.PlayerName != "")
        {
            PlayerName = DontDestroyWhenReload.Instance.PlayerName;
        }
    }

    void Update()
    {
    }
   

    public float GetCropGoldAmount(int amount)
    {
        return (amount * (1 + BuffManager.CropGoldBuff));
    }
    public float GetMaterialGoldAmount(int amount)
    {
        return (amount * (1 + BuffManager.MaterialGoldBuff));
    }


    public bool IsSuccess(float chance)
    {
        float isDrop = Random.Range(0f, 1f);
        return isDrop <= chance;
    }
}

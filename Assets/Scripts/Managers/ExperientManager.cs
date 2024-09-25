using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExperientManager : MonoBehaviour
{
    public int Level = 1;
    public int Experient = 0;
    public int ExperientToNextLevel = 50;
    public float ExperientMultiplier = 1.5f;

    public Image Fillbar;
    public TextMeshProUGUI currentLevelText;
    private GameManager gameManager;

    public GameObject ExpBar;

    public NewBuffManager BuffManager;

    public GameObject XPPopup;
    public GameObject Canvas;
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        BuffManager = FindAnyObjectByType<NewBuffManager>();
        ExpBar.gameObject.SetActive(true);
        gameManager = GetComponentInParent<GameManager>();
        Fillbar.fillAmount = GlobalVariables.UpdateFillBar(Experient, ExperientToNextLevel);
    }

    void Update()
    {
        if (Experient >= ExperientToNextLevel)
        {
            LevelUp();
        }
        Fillbar.fillAmount = GlobalVariables.UpdateFillBar(Experient, ExperientToNextLevel);
        currentLevelText.text = Level.ToString();
    }

    public void LevelUp()
    {
        Level++;
        Experient = 0;
        ExperientToNextLevel = (int) (ExperientToNextLevel * ExperientMultiplier);
        BuffManager.ToggleBuffShopUI();
    }

    public void GainExperient(int amount)
    {

        Experient += GetExperientGain(amount);
    }

    public int GetExperientGain(int amount)
    {
        return (int)((amount * (1 + gameManager.ExperientBuff)));
    }

    public void SpawnExp(int amount, Vector3 position)
    {
        if (Canvas != null && XPPopup != null)
        {
            //experientManager.Experient = (int)(experientManager.Experient + SeedData.SeedProduct.GetTotalExperience());
            GainExperient(amount);
            GameObject popup = Instantiate(XPPopup, position, Quaternion.identity, Canvas.transform);
            TextPopup goldPopup = popup.GetComponent<TextPopup>();
            goldPopup.isAdd = false;
            goldPopup.isGold = false;
            goldPopup.goldPopup = GetExperientGain(amount);
        }
    }
}

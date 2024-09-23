using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public BuffManager BuffManager;
    void Start()
    {
        BuffManager = FindAnyObjectByType<BuffManager>();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperientManager : MonoBehaviour
{
    public int Level;
    public int Experient = 0;
    public int ExperientToNextLevel = 50;
    public float ExperientMultiplier = 1.5f;

    public Image Fillbar;
    private GameManager gameManager;

    public GameObject BuffWindow;
    void Start()
    {
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

    }

    public void LevelUp()
    {
        Level++;
        Experient = 0;
        ExperientToNextLevel = (int) (ExperientToNextLevel * ExperientMultiplier);
        BuffWindow.SetActive(true);
    }
}

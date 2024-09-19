using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image Fillbar;
    private GameManager gameManager;
    public float review = 0;
    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
        Fillbar.fillAmount =GlobalVariables.UpdateFillBar(gameManager.CurrentEnergy, gameManager.Energy);
    }

    // Update is called once per frame
    void Update()
    {
        Fillbar.fillAmount = GlobalVariables.UpdateFillBar(gameManager.CurrentEnergy, gameManager.Energy);
    }

}

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
        UpdateFillBar();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFillBar();
    }
    public void UpdateFillBar()
    {
        review = (float)gameManager.CurrentEnergy / (float)gameManager.Energy;
        Fillbar.fillAmount = review;
    }
}

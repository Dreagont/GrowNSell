using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public bool isWatered = false;
    public float waterTimer = 0;
    void Start()
    {
        
    }

    void Update()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        
    }

    public void DeWater()
    {
        this.isWatered = false;
        waterTimer = 0;
    }

    public void Wartering()
    {
        this.isWatered = true;
        waterTimer += Time.deltaTime * GlobalVariables.timeMultiplier;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public GameObject waterd;
    public GameObject noWatered;
    public bool isWatered = false;
    void Start()
    {
        
    }

    void Update()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        if (isWatered)
        {
            waterd.SetActive(true);
            noWatered.SetActive(false);
        }
        else
        {
            waterd.SetActive(false);
            noWatered.SetActive(true);
        }
    }

    public void DeWater()
    {
        this.isWatered = false;
    }
}

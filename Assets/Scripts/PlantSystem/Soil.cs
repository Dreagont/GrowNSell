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
        if (isWatered)
        {
            waterd.SetActive(true);
            noWatered.SetActive(false);
        } else
        {
            waterd.SetActive(false);
            noWatered.SetActive(true);
        }
    }
}

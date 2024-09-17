using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Gold = 1000;

    public int Energy = 100;
    public int CurrentEnergy = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CanAffordItem(int itemPrice)
    {
        return Gold - itemPrice >= 0;
    }
}

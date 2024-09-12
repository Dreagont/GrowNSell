using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public SeedData SeedData;
    private float growStates = 10;
    private float periodStates;
    private int currentState = 0;
    public GameObject[] States;
    public int secondChangeState;
    public float timePlanted;
    public Soil thisSoil;
    void Start()
    {
        timePlanted = GlobalVariables.TimeCounter;

        periodStates = SeedData.GrowTime/ growStates ;
        
        secondChangeState = (int)(120 * periodStates);

        UpdateSeedSprite();
    }

    void Update()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (currentState < growStates - 1)
        {
            if (GlobalVariables.TimeCounter  - timePlanted >= secondChangeState - 1 && thisSoil.isWatered)
            {
                currentState++;
                UpdateSeedSprite();
                timePlanted = GlobalVariables.TimeCounter;
            }
        }
    }

    public void UpdateSeedSprite()
    {
        for (int i = 0; i < States.Length; i++)
        {
            if (i != currentState)
            {
                States[i].gameObject.SetActive(false);
            }
        }
        States[currentState].gameObject.SetActive(true);   
    }


}

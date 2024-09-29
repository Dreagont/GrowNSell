using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public SeedData SeedData;
    public int periodStates;
    public int currentState = 0;
    public GameObject[] States;
    //public int secondChangeState;
    public int[] distribution;
    public int currentPeriod = 0;
    public Soil thisSoil;
    public bool Harvestable = false;
    public Vector3 position;
    public int temp = 0;
    public ExperientManager experientManager;
    public ObjectsManager objectsManager;
    public bool isDestroyed = false;
    public GameObject[] HarvestOuline;
    public int dayNotWater = 0;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        setOuline();
        experientManager = FindAnyObjectByType<ExperientManager>();
        objectsManager = FindAnyObjectByType<ObjectsManager>();
        //periodStates = SeedData.GrowTime / growStates;
        int remainder = States.Length % SeedData.GrowTime;
        int baseAnimation = States.Length / SeedData.GrowTime;
        distribution = new int[SeedData.GrowTime];
        for (int i = 0; i < SeedData.GrowTime; i++)
        {
            if (i < remainder)
            {
                distribution[i] = (baseAnimation + 1);
            } else
            {
                distribution[i] = (baseAnimation);
            }
        }

        for (int i = 0; i < States.Length; i++)
        {
            States[i].SetActive(i == currentState);
        }

        //secondChangeState = (int)(120 * periodStates);
        //UpdateSeedSprite();
    }

    void Update()
    {
        if (Harvestable)
        {
            foreach (var item in HarvestOuline)
            {
                item.SetActive(true);
            }
        }
        DestroyAfterNotWater();
    }

    public void DestroyAfterNotWater()
    {
        if (dayNotWater >3 && !Harvestable)
        {
            objectsManager.SeedValues.Remove(position);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void setOuline()
    {
        Vector3 newPosition1 = HarvestOuline[0].transform.localPosition;
        newPosition1.x = 0.065f;
        newPosition1.y = 0;
        HarvestOuline[0].transform.localPosition = newPosition1;

        Vector3 newPosition2 = HarvestOuline[1].transform.localPosition;
        newPosition2.x = -0.065f;
        newPosition2.y = 0;
        HarvestOuline[1].transform.localPosition = newPosition2;

        Vector3 newPosition3 = HarvestOuline[2].transform.localPosition;
        newPosition3.y = 0.065f;
        newPosition3.x = 0;
        HarvestOuline[2].transform.localPosition = newPosition3;

        Vector3 newPosition4 = HarvestOuline[3].transform.localPosition;
        newPosition4.y = -0.065f;
        newPosition4.x = 0;
        HarvestOuline[3].transform.localPosition = newPosition4;
    }


    public void ChangeSprite()
    {
        temp = distribution[currentPeriod];
        currentPeriod++;
    }

    public void UpdateSeedSprite()
    {
        currentState += distribution[currentPeriod];
        currentPeriod++;
        if (currentPeriod > distribution.Length - 1)
        {
            currentPeriod = distribution.Length -1;
        }
        currentState = Mathf.Min(currentState, States.Length - 1);  

        for (int i = 0; i < States.Length; i++)
        {
            States[i].SetActive(i == currentState);
        }

        temp = 0;
        if (currentState == States.Length - 1)
        {
            Harvestable = true;
        }
    }

    private void OnDestroy()
    {
        int gainAmount = SeedData.Experient;
        if (isDestroyed == false)
        {
            if (gameManager.IsSuccess(SeedData.DoubleExp))
            {
                gainAmount *= 2;
            }

            Debug.Log(SeedData.DoubleExp);

            experientManager.SpawnExp(gainAmount,position);
        }
    }
}

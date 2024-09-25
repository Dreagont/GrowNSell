using UnityEngine;

public class Seed : MonoBehaviour
{
    public SeedData SeedData;
    private float growStates = 10;
    private float periodStates;
    public int currentState = 0;
    public GameObject[] States;
    public int secondChangeState;
    public float timePlanted;
    public Soil thisSoil;
    public bool Harvestable = false;
    public Vector3 position;
    public int temp = 0;
    public ExperientManager experientManager;
    public ObjectsManager objectsManager;
    public bool isDestroyed = false;
    public GameObject[] HarvestOuline;
    public int dayNotWater = 0;
    void Start()
    {
        setOuline();
        experientManager = FindAnyObjectByType<ExperientManager>();
        objectsManager = FindAnyObjectByType<ObjectsManager>();
        timePlanted = (GlobalVariables.currentDay - 1) * 120f;
        periodStates = SeedData.GrowTime / growStates;
        secondChangeState = (int)(120 * periodStates);
        UpdateSeedSprite();
    }

    void Update()
    {
        ChangeSprite();
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
        if (currentState < growStates - 1)
        {
            float timeSinceLastUpdate = GlobalVariables.TimeCounter - timePlanted;

            int statesToAdvance = (int)(timeSinceLastUpdate / secondChangeState);

            if (statesToAdvance > 0)
            {
                temp += statesToAdvance;
                timePlanted += statesToAdvance * secondChangeState; 
            }
        }
    }

    public void UpdateSeedSprite()
    {
        currentState += temp;  
        currentState = Mathf.Min(currentState, States.Length - 1);  

        for (int i = 0; i < States.Length; i++)
        {
            States[i].SetActive(i == currentState);
        }

        temp = 0;
        if (currentState == growStates - 1)
        {
            Harvestable = true;
        }
    }

    private void OnDestroy()
    {
        if (isDestroyed == false)
        {
            experientManager.SpawnExp(SeedData.SeedProduct.Experient, position);
        }
    }
}

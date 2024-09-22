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
    public GameObject XPPopup;
    public GameObject Canvas;

    void Start()
    {
        experientManager = FindAnyObjectByType<ExperientManager>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        timePlanted = (GlobalVariables.currentDay - 1) * 120f;
        periodStates = SeedData.GrowTime / growStates;
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
            float timeSinceLastUpdate = GlobalVariables.TimeCounter - timePlanted;

            int statesToAdvance = (int)(timeSinceLastUpdate / secondChangeState);

            if (statesToAdvance > 0)
            {
                temp += statesToAdvance;
                timePlanted += statesToAdvance * secondChangeState; // Update the planted time
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
        experientManager.Experient = (int)(experientManager.Experient + SeedData.SeedProduct.GetTotalExperience());
        if (Canvas != null && XPPopup != null)
        {
            GameObject popup = Instantiate(XPPopup, position, Quaternion.identity, Canvas.transform);
            TextPopup goldPopup = popup.GetComponent<TextPopup>();
            goldPopup.isAdd = false;
            goldPopup.isGold = false;
            goldPopup.goldPopup = SeedData.SeedProduct.Experient;
        }
    }
}

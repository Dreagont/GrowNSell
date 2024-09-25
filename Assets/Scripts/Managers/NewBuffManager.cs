using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuffManager : MonoBehaviour
{
    public GameObject BuffShopUI;
    public GameObject FadeScreen;

    public GameObject buffSlotPrefab;
    public Transform buffSlotParent;
    public List<GameObject> allAvailableBuffs;
    public List<GameObject> buffsForSale;
    public int numberOfBuffsToDisplay = 3;

    public GameManager GameManager;
    void Start()
    {
        RollNewBuffs();
        PopulateBuffShop();
        ResetAllBuff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetAllBuff()
    {
        GameManager.newBuffs.Clear();
    }

    public void RollNewBuffs()
    {
        buffsForSale.Clear();

        List<GameObject> availableBuffs = new List<GameObject>(allAvailableBuffs);

        for (int i = 0; i < numberOfBuffsToDisplay; i++)
        {
            if (availableBuffs.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableBuffs.Count);
            GameObject randomBuff = availableBuffs[randomIndex];

            buffsForSale.Add(randomBuff);
            availableBuffs.RemoveAt(randomIndex);
        }

        PopulateBuffShop();
    }

    private void PopulateBuffShop()
    {
        foreach (Transform child in buffSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject buff in buffsForSale)
        {
            GameObject slot = Instantiate(buff, buffSlotParent);
        }
    }

    public void OnRollButtonPressed()
    {
        RollNewBuffs();
    }

    public void ToggleBuffShopUI()
    {
        if (BuffShopUI.activeInHierarchy)
        {
            BuffShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
            FadeScreen.SetActive(false);
        }
        else
        {
            BuffShopUI.SetActive(true);
            GlobalVariables.CanAction = false;
            FadeScreen.SetActive(true);
            RollNewBuffs();
        }
    }
}

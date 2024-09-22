using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public InventoryItemData[] itemCanApplyBuff;

    public InventoryItemData itemData;
    public Buff Buff;

    public GameObject BuffShopUI;
    public GameObject FadeScreen;

    public GameObject buffSlotPrefab;
    public Transform buffSlotParent;
    public List<Buff> allAvailableBuffs;
    public List<Buff> buffsForSale;
    public int numberOfBuffsToDisplay = 3;

    void Start()
    {
        RollNewBuffs();
        PopulateBuffShop();
        ResetAllBuff();
    }

    void Update()
    {
        
    }

    public void ApplyBuff(InventoryItemData item, Buff buff)
    {
        item.AddBuff(buff);
    }

    public void ResetAllBuff()
    {
        foreach (var item in itemCanApplyBuff)
        {
            item.ResetBuff();
        }
    }

    private void PopulateBuffShop()
    {
        foreach (Transform child in buffSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Buff buff in buffsForSale)
        {
            GameObject slot = Instantiate(buffSlotPrefab, buffSlotParent);
            BuffUI buffUI = slot.GetComponent<BuffUI>();
            buffUI.buff = buff;
            buffUI.UpdateBuffUI();
        }
    }

    public void RollNewBuffs()
    {
        buffsForSale.Clear();

        List<Buff> availableBuffs = new List<Buff>(allAvailableBuffs);

        for (int i = 0; i < numberOfBuffsToDisplay; i++)
        {
            if (availableBuffs.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableBuffs.Count);
            Buff randomBuff = availableBuffs[randomIndex];

            buffsForSale.Add(randomBuff);
            availableBuffs.RemoveAt(randomIndex);  
        }

        PopulateBuffShop();
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

    public void OnRollButtonPressed()
    {
        RollNewBuffs();
    }
}

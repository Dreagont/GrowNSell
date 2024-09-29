using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    public Buff buff;
    public TextMeshProUGUI buffNameText;
    public TextMeshProUGUI buffDescriptionText;
    public Image buffSprite;
    public BuffManager buffManager;
    public bool canClick = false;
    void Start()
    {
        buffManager = FindAnyObjectByType<BuffManager>();
        UpdateBuffUI();
        StartCoroutine(ClickCooldown());
    }
    void Update()
    {
        
    }

    public void OnBuffClicked()
    {
        if (canClick)
        {
            ApplyBuff(buff.itemBuffed, buff);
            //buffManager.ToggleBuffShopUI();
        }
        
    }

    public void ApplyBuff(InventoryItemData item, Buff buff)
    {
        //item.AddBuff(buff);
    }

    public void UpdateBuffUI()
    {
        buffNameText.text = buff.buffName;
        buffDescriptionText.text = buff.description;
        buffSprite.sprite = buff.buffIcon;
    }
    private IEnumerator ClickCooldown()
    {
        canClick = false; 
        yield return new WaitForSeconds(1.5f); 
        canClick = true;
    }
}

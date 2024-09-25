using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBuffUI : MonoBehaviour
{
    public NewBuff buff;
    public TextMeshProUGUI buffNameText;
    public TextMeshProUGUI buffDescriptionText;
    public Image buffSprite;
    public BuffManager buffManager;
    public bool canClick = false;
    GameManager GameManager;
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
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
            GameManager.AddBuff(buff);
            buffManager.ToggleBuffShopUI();
        }

    }


    public void UpdateBuffUI()
    {
        buffNameText.text = buff.buffName;
        buffDescriptionText.text = buff.description;
        buffSprite.sprite = buff.BuffIcon;
    }
    private IEnumerator ClickCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(1.5f);
        canClick = true;
    }
}

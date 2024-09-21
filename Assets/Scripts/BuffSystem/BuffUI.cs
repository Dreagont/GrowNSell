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
    void Start()
    {
        UpdateBuffUI();
    }
    void Update()
    {
        
    }

    private void UpdateBuffUI()
    {
        buffNameText.text = buff.buffName;
        buffDescriptionText.text = buff.description;
        buffSprite.sprite = buff.buffIcon;
    }
}

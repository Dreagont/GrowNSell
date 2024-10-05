using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockItemUI : MonoBehaviour
{
    public Image ItemIcon;
    public TextMeshProUGUI ItemPrice;
    private GameManager gameManager;
    private Color baseColor;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        baseColor = new Color32(0x43, 0x5B, 0x44, 0xFF);
    }

    void Update()
    {
        if (ItemPrice != null) 
        {
            if (!gameManager.GoldManager.CanAffordItem(int.Parse(ItemPrice.text)))
            {
                ItemPrice.color = Color.red;
            } else
            {
                ItemPrice.color = baseColor;
            }
        }
    }

    public void SetPrice(int price)
    {
        if (price < 10000)
        {
            ItemPrice.text = price.ToString();
        } else
        {
            ItemPrice.text = GlobalVariables.FormatNumber(price);
        }
    }
}

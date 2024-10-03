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
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (ItemPrice != null) 
        {
            if (!gameManager.GoldManager.CanAffordItem(int.Parse(ItemPrice.text)))
            {
                ItemPrice.color = Color.red;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    public Image ItemIcon;
    public InventoryItemData InventoryItemData;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemPrice;
    public TextMeshProUGUI sellQuantityText;
    private PlayerInventoryHolder playerInventoryHolder;
    private GameManager gameManager;
    public int sellQuanity;
    public GameObject goldAddText;
    public Canvas canvas;
    void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        playerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        gameManager = FindObjectOfType<GameManager>();
        SetUi();
    }

    void Update()
    {
        if (sellQuanity == 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetUi()
    {
        ItemIcon.sprite = InventoryItemData.icon;
        ItemName.text = InventoryItemData.displayName;
        ItemPrice.text = GlobalVariables.FormatNumber(InventoryItemData.buyPrice);
        sellQuantityText.text = sellQuanity.ToString();
    }

    public void BuyItem()
    {
        if (gameManager.CanAffordItem(InventoryItemData.buyPrice))
        {
            if (playerInventoryHolder.AddToHotBar(InventoryItemData, 1))
            {
                sellQuanity--;
                SetUi();
                gameManager.Gold -= InventoryItemData.buyPrice;
                Vector3 mousePosition = Input.mousePosition;
                GameObject popup = Instantiate(goldAddText, mousePosition, Quaternion.identity, canvas.transform);
                TextPopup goldPopup = popup.GetComponent<TextPopup>();
                goldPopup.isAdd = false;
                goldPopup.isGold = true;
                goldPopup.goldPopup = InventoryItemData.buyPrice;
            } else
            {
                return;
            }
        }
    }
}

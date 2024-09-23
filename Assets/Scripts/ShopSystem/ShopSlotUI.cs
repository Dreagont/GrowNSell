using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
    private Coroutine showTooltipCoroutine;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InventoryItemData != null)
        {
            showTooltipCoroutine = StartCoroutine(ShowTooltipDelayed());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (showTooltipCoroutine != null)
        {
            StopCoroutine(showTooltipCoroutine);
        }
        TooltipManager.instance.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            BuyAllItems();
        }
    }

    public void BuyAllItems()
    {
        int maxAffordableQuantity = Mathf.FloorToInt(gameManager.Gold / InventoryItemData.buyPrice); 
        int quantityToBuy = Mathf.Min(maxAffordableQuantity, sellQuanity); 

        if (quantityToBuy > 0)
        {
            if (playerInventoryHolder.AddToHotBar(InventoryItemData, quantityToBuy))
            {
                sellQuanity -= quantityToBuy; 
                gameManager.Gold -= quantityToBuy * InventoryItemData.buyPrice;

                SetUi();

                Vector3 mousePosition = Input.mousePosition;
                GameObject popup = Instantiate(goldAddText, mousePosition, Quaternion.identity, canvas.transform);
                TextPopup goldPopup = popup.GetComponent<TextPopup>();
                goldPopup.isAdd = false;
                goldPopup.isGold = true;
                goldPopup.goldPopup = quantityToBuy * InventoryItemData.buyPrice;
            }
        }
    }


    private IEnumerator ShowTooltipDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        if (InventoryItemData != null)
        {
            TooltipManager.instance.SetAndShowToolTip(InventoryItemData.displayName, InventoryItemData.description);

        }
    }
}

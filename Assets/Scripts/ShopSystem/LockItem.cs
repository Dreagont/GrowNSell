using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public LockItemData LockItemData;
    private Coroutine showTooltipCoroutine;
    public ShopUIController ShopUIController;
    public GameManager GameManager;
    private IslandExpander IslandExpander;
    private PlayerInventoryHolder PlayerInventoryHolder;   
    void Start()
    {
        PlayerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        IslandExpander = FindAnyObjectByType<IslandExpander>(); 
        GameManager = FindAnyObjectByType<GameManager>();
        ShopUIController = FindAnyObjectByType<ShopUIController>();
    }

    void Update()
    {
        
    }

    public void UnlockItem()
    {
        if (LockItemData.triggerIndex == 0)
        {
            if (GameManager.GoldManager.CanAffordItem(LockItemData.Price) && PlayerInventoryHolder.CraftableItem(LockItemData.craftingMaterials))
            {
                GameManager.GoldManager.SpawnGoldText(-LockItemData.Price, false, 1);
                PlayerInventoryHolder.RemoveItemForCraft(LockItemData.craftingMaterials);
                IslandExpander.GenerateArena(10, 10, 1);
                Destroy(gameObject);
                return;
            }
            
        } else if (GameManager.GoldManager.CanAffordItem(LockItemData.Price))
        {
            GameManager.GoldManager.SpawnGoldText(-LockItemData.Price, false, 1);
            ShopUIController.allAvailableItems.Add(LockItemData.lockItem);
            Destroy(gameObject);
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (LockItemData != null)
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
            
        }
    }
    private IEnumerator ShowTooltipDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        if (LockItemData != null)
        {

            TooltipManager.instance.SetAndShowToolTipLock(LockItemData.ItemName, LockItemData.ItemDescription, LockItemData.craftingMaterials);
        }
    }
}

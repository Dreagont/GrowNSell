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
    private LockItemUI lockItemUI;
    private LockItemList LockItemList;
    void Start()
    {
        LockItemList = FindAnyObjectByType<LockItemList>();
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
            
        } else if (LockItemData.triggerIndex == 1)
        {
            ShopUIController.OnButtonUpgradePressed();
            lockItemUI = GetComponent<LockItemUI>();
            lockItemUI.SetPrice(GameManager.ShopUpgradeCost);
        } else
        if (GameManager.GoldManager.CanAffordItem(LockItemData.Price))
        {
            GameManager.GoldManager.SpawnGoldText(-LockItemData.Price, false, 1);
            foreach (var item in LockItemData.lockItem)
            {
                ShopUIController.allAvailableItems.Add(item);
            }
            Destroy(gameObject);
        }
    }

    public void CraftingItem()
    {
        if (PlayerInventoryHolder.CraftableItem(LockItemData.craftingMaterials))
        {
            PlayerInventoryHolder.RemoveItemForCraft(LockItemData.craftingMaterials);

            if (LockItemData.triggerIndex == 0)
            {
                foreach (var item in LockItemData.lockItemDatas)
                {
                    LockItemList.CraftingItems.Add(item);
                    Destroy(gameObject);
                    LockItemList.CraftingItems.Remove(this.LockItemData);
                    LockItemList.PopulateCraftingPanel();
                }
            } else
            {
                foreach (var item in LockItemData.lockItem)
                {
                    PlayerInventoryHolder.AddToHotBar(item, 1);
                }
            }
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

    private void OnDestroy()
    {
        TooltipManager.instance.HideTooltip();
    }
}

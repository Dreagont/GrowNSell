using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUi : MonoBehaviour
    , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public Image holder;
    public EquipableTag equipableTag = EquipableTag.None;
    [SerializeField] private InventorySlots assignedInventorySlot;
    public InventorySlots AssignedInventorySlot => assignedInventorySlot;

    private Button button;

    public InventoryDisplay ParentDisplay { get; private set; }

    private Coroutine showTooltipCoroutine;

    public Image IsSelected;

    public ShopSelling ShopSelling;

    private GameManager gameManager;

    private void Awake()
    {
        IsSelected.gameObject.SetActive(false);
        ClearSlot();
        gameManager = FindAnyObjectByType<GameManager>();
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
        ShopSelling = FindAnyObjectByType<ShopSelling>();
    }

    public void Init(InventorySlots slot)
    {
        assignedInventorySlot = slot;
        UpdateInventorySlot(slot);
    }

    public void UpdateInventorySlot(InventorySlots slot)
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.icon;
            itemSprite.color = Color.white;
            holder.color = Color.clear;
            if (slot.StackSize == 0)
            {
                ClearSlot();
            } else
            {
                itemCount.text = slot.StackSize > 1 ? slot.StackSize.ToString() : "";
            }
        }
        else
        {
            ClearSlot();
        }

    }

    public void UpdateInventorySlot()
    {
        if (assignedInventorySlot != null)
        {
            UpdateInventorySlot(assignedInventorySlot);
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
        if (equipableTag != EquipableTag.None)
        {
            holder.color = Color.white;
        }
    }

    public void ClearSlot(InventorySlots slots)
    {
        slots.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void OnUISlotClick()
    {
        if (GlobalVariables.canSwapSlot)
        {
            ParentDisplay?.SlotClicked(this);
        }
    }

    public void SellInventoryItem()
    {
        if (ShopSelling !=  null)
        {
            Debug.Log("sell out");

            ShopSelling.SellItem(this.assignedInventorySlot);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (assignedInventorySlot.ItemData != null)
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
            ParentDisplay?.SplitStack(this);
        } 
    }

    private IEnumerator ShowTooltipDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        if (assignedInventorySlot.ItemData != null)
        {

            if (assignedInventorySlot.ItemData.itemType1 == ItemType.Crop)
            {
                TooltipManager.instance.SetAndShowToolTip(assignedInventorySlot.ItemData.displayName, assignedInventorySlot.ItemData.description, (int)(gameManager.GetCropGoldAmount(assignedInventorySlot.ItemData.sellPrice)));

            }
            else if (assignedInventorySlot.ItemData.itemType1 == ItemType.Material)
            {
                TooltipManager.instance.SetAndShowToolTip(assignedInventorySlot.ItemData.displayName, assignedInventorySlot.ItemData.description, (int)(gameManager.GetMaterialGoldAmount(assignedInventorySlot.ItemData.sellPrice)));

            } else
            {
                TooltipManager.instance.SetAndShowToolTip(assignedInventorySlot.ItemData.displayName, assignedInventorySlot.ItemData.description, (int)(gameManager.GetCropGoldAmount(assignedInventorySlot.ItemData.sellPrice)));
            }

        }
    }
}

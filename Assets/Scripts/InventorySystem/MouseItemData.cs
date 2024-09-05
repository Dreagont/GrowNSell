using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlots AsssignedInventorySlot;

    public Canvas parentCanvas;

    private void Start()
    {

    }
    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        ClearSlot();
    }

    public void UpdateMouseSlot(InventorySlots invSlot)
    {
        AsssignedInventorySlot = invSlot;
        ItemSprite.sprite = invSlot.ItemData.icon;
        ItemCount.text = invSlot.StackSize > 1 ? invSlot.StackSize.ToString() : "";
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AsssignedInventorySlot != null && AsssignedInventorySlot.ItemData != null)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, mousePosition, parentCanvas.worldCamera, out canvasPosition);
            transform.position = parentCanvas.transform.TransformPoint(canvasPosition);

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
        }
    }

    public void ClearSlot()
    {
        AsssignedInventorySlot.ClearSlot();
        AsssignedInventorySlot = null;
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

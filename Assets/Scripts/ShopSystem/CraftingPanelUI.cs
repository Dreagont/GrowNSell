using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftingPanelUI : MonoBehaviour
{
    public GameObject CraftingUi;
    public GameObject FadeScreen;
    private void Start()
    {
        CraftingUi.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && CraftingUi.activeInHierarchy)
        {
            ToggleShopUI();
        } 
    }

    public void ToggleShopUI()
    {
        if (CraftingUi.activeInHierarchy)
        {
            CraftingUi.SetActive(false);
            GlobalVariables.CanAction = true;
            FadeScreen.gameObject.SetActive(false);
        }
        else
        {
            CraftingUi.SetActive(true);
            GlobalVariables.CanAction = false;
            FadeScreen.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        TooltipManager.instance.HideTooltip();
    }
}

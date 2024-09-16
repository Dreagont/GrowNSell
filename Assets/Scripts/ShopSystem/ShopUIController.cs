using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUIController : MonoBehaviour
{
    public GameObject ShopUI;
    void Start()
    {
        ShopUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
        }
    }

    public void ToggleShopUI()
    {
        if (ShopUI.activeInHierarchy)
        {
            ShopUI.SetActive(false);
            GlobalVariables.CanAction = true;
        } else
        {
            ShopUI.SetActive(true);
            GlobalVariables.CanAction = false;
        }
    }
}

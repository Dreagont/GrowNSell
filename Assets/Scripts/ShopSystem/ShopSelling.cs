using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelling : MonoBehaviour
{
    private PlayerInventoryHolder inventoryHolder;
    private GameManager gameManager;
    public GameObject goldAddText;
    public Transform canvas;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        inventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
    }

    public void SellItem()
    {
        if (inventoryHolder != null && inventoryHolder.SellInventorySystem != null)
        {
            if (inventoryHolder.SellInventorySystem.InventorySlots.Count > 0)
            {
                InventorySlots sellSlot = inventoryHolder.SellInventorySystem.InventorySlots[0];

                if (sellSlot != null && sellSlot.ItemData != null)
                {
                    int goldToAdd = (int)(sellSlot.StackSize * sellSlot.ItemData.GetTotalSellPrice());
                    gameManager.Gold += goldToAdd;
                    sellSlot.RemoveFromStack(sellSlot.StackSize);
                    Debug.Log("Added " + goldToAdd);

                    Vector3 mousePosition = Input.mousePosition;
                    GameObject popup = Instantiate(goldAddText, mousePosition, Quaternion.identity, canvas);
                    TextPopup goldPopup = popup.GetComponent<TextPopup>();
                    goldPopup.isGold = true;
                    goldPopup.isAdd = true;
                    goldPopup.goldPopup = goldToAdd;
                }
                else
                {
                    Debug.LogWarning("The slot is empty or does not contain any item data.");
                }
            }
            else
            {
                Debug.LogWarning("No inventory slots available.");
            }
        }
        else
        {
            Debug.LogError("Inventory holder or SellInventorySystem is not initialized.");
        }
    }

}

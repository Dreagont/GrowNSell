using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelling : MonoBehaviour
{
    private PlayerInventoryHolder inventoryHolder;
    public GameManager gameManager;

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
                    int goldToAdd = 0;
                    if (sellSlot.ItemData.itemType1 == ItemType.Crop || sellSlot.ItemData.itemType2 == ItemType.Crop)
                    {
                        goldToAdd = (int)(gameManager.GetCropGoldAmount(sellSlot.ItemData.sellPrice));
                    } else if (sellSlot.ItemData.itemType1 == ItemType.Material || sellSlot.ItemData.itemType2 == ItemType.Material)
                    {
                        goldToAdd = (int)(gameManager.GetMaterialGoldAmount(sellSlot.ItemData.sellPrice));
                    } else
                    {
                        goldToAdd = sellSlot.ItemData.sellPrice;
                    }
                    gameManager.GoldManager.SpawnGoldText(goldToAdd, true, sellSlot.StackSize);

                    sellSlot.RemoveFromStack(sellSlot.StackSize);
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

    public void SellItem(InventorySlots sellSlot)
    {     
        if (sellSlot != null && sellSlot.ItemData != null)
        {
            int goldToAdd = (int)(sellSlot.StackSize * sellSlot.ItemData.sellPrice);
            gameManager.GoldManager.SpawnGoldText(-goldToAdd, true, 1);

            sellSlot.RemoveFromStack(sellSlot.StackSize);
            Debug.Log("Added " + goldToAdd);

            gameManager.GoldManager.SpawnGoldText(sellSlot.ItemData.sellPrice, true, sellSlot.StackSize);
        }
        else
        {
            Debug.LogWarning("The slot is empty or does not contain any item data.");
        }
            
    }

}

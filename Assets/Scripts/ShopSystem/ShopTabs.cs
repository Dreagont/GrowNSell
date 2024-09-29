using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabs : MonoBehaviour
{
    public GameObject ShopTab;
    public GameObject PricesTab;
    public GameObject UnlockTab;
    public Image ShopImage;
    public Image PriceImage;
    public Image UnlockImage;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToggleBetweenTabs(int index)
    {
        if (index == 0) // Shop tab
        {
            ShopTab.SetActive(true);
            PricesTab.SetActive(false);
            UnlockTab.SetActive(false);

            Vector3 pos1 = PriceImage.gameObject.transform.localPosition;
            pos1.y = 0;
            PriceImage.gameObject.transform.localPosition = pos1;

            Vector3 pos = ShopImage.gameObject.transform.localPosition;
            pos.y = -9;
            ShopImage.gameObject.transform.localPosition = pos;

            Vector3 pos2 = UnlockImage.gameObject.transform.localPosition;
            pos2.y = 0;
            UnlockImage.gameObject.transform.localPosition = pos2;
        }
        else if (index == 1) // Prices tab
        {
            ShopTab.SetActive(false);
            PricesTab.SetActive(true);
            UnlockTab.SetActive(false);

            Vector3 pos1 = ShopImage.gameObject.transform.localPosition;
            pos1.y = 0;
            ShopImage.gameObject.transform.localPosition = pos1;

            Vector3 pos = PriceImage.gameObject.transform.localPosition;
            pos.y = -9;
            PriceImage.gameObject.transform.localPosition = pos;

            Vector3 pos2 = UnlockImage.gameObject.transform.localPosition;
            pos2.y = 0;
            UnlockImage.gameObject.transform.localPosition = pos2;
        }
        else if (index == 2) // Unlock tab
        {
            ShopTab.SetActive(false);
            PricesTab.SetActive(false);
            UnlockTab.SetActive(true);

            Vector3 pos1 = ShopImage.gameObject.transform.localPosition;
            pos1.y = 0;
            ShopImage.gameObject.transform.localPosition = pos1;

            Vector3 pos2 = PriceImage.gameObject.transform.localPosition;
            pos2.y = 0;
            PriceImage.gameObject.transform.localPosition = pos2;

            Vector3 pos = UnlockImage.gameObject.transform.localPosition;
            pos.y = -9;
            UnlockImage.gameObject.transform.localPosition = pos;
        }
    }


}

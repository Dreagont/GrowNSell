using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemPrice;
    public RectTransform rectTransform;
    public GameObject PriceBox;
    public GameObject DescriptonBox;
    public GameObject Material;
    public Transform MaterialBox;
    public GameObject MaterialItem;
    public float xOffset = 20f;
    public float yOffset = 20f;
    public PlayerInventoryHolder PlayerInventoryHolder;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        PlayerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 position = Input.mousePosition;
        position.x += xOffset;
        position.y += yOffset;

        // Ensure the tooltip stays within screen bounds
        Vector2 screenBounds = new Vector2(Screen.width, Screen.height);
        position.x = Mathf.Clamp(position.x, 0, screenBounds.x - rectTransform.rect.width);
        position.y = Mathf.Clamp(position.y, 0, screenBounds.y - rectTransform.rect.height);

        transform.position = position;
    }

    public void SetAndShowToolTip(string name, string description, int price)
    {
        if (price == -1)
        {
            PriceBox.SetActive(false);
        }
        else
        {
            PriceBox.SetActive(true);
            ItemPrice.text = price.ToString();
        }

        if (description == "")
        {
            DescriptonBox.SetActive(false);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 100);
        } else
        {
            DescriptonBox.SetActive(true);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 360);
        }
        gameObject.SetActive(true); 
        ItemName.text = name;
        ItemDescription.text = description;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void SetAndShowToolTipLock(string name, string description, CraftingMaterial[] materials)
    {
        if (materials.Length != 0)
        {
            Material.SetActive(true);

            foreach (var material in materials)
            {
                GameObject item = Instantiate(MaterialItem, MaterialBox);
                CraftingMaterialUI materialUI = item.GetComponent<CraftingMaterialUI>();
                materialUI.ItemIcon.sprite = material.InventoryItemData.icon;
                materialUI.Quantity.text = material.Quantity.ToString();

                if (PlayerInventoryHolder.HaveEnoughItem(material))
                {
                    materialUI.Quantity.color = Color.green;
                } else
                {
                    materialUI.Quantity.color = Color.red;
                }
            }
        }

        if (description == "")
        {
            DescriptonBox.SetActive(false);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 100);
        }
        else
        {
            DescriptonBox.SetActive(true);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 360);
        }
        
        gameObject.SetActive(true);
        PriceBox.SetActive(false);
        ItemName.text = name;
        ItemDescription.text = description;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }


    public void HideTooltip()
    {
        gameObject.SetActive(false);
        ItemName.text = string.Empty;
        ItemDescription.text = string.Empty;
    }

    private void OnDisable()
    {
        Material.SetActive(false);
        foreach (Transform child in MaterialBox)
        {
            Destroy(child.gameObject);
        }
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public Image Icon;
    public RectTransform rectTransform;
    public float xOffset = 20f;
    public float yOffset = 20f;

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

    public void SetAndShowToolTip(Sprite icon, string name, string description)
    {
        gameObject.SetActive(true);
        ItemName.text = name;
        ItemDescription.text = description;
        Icon.sprite = icon;

        // Force layout update
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        ItemName.text = string.Empty;
        ItemDescription.text = string.Empty;
    }
}
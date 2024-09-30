using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlaceAbleObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public InventoryItemData placeAbleObjectData;
    public TileManager TileManager;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        TileManager = FindAnyObjectByType<TileManager>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (spriteRenderer != null && placeAbleObjectData != null)
        {
            spriteRenderer.sprite = placeAbleObjectData.icon; 
        }
        Debug.Log("Start called");
    }

    void Update()
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered!");
        Vector3 addPos = gameObject.transform.position - new Vector3(0.5f, 0.5f, 0);
        Vector3Int position = Vector3Int.RoundToInt(addPos);
        TileManager.isPlaceObject = true;
        TileManager.HighlightTileUnderMouse(position, placeAbleObjectData.PlaceAbleObject.Radius);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileManager.isPlaceObject = false;
    }
}

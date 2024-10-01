using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class PlaceAbleObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public InventoryItemData ItemPlaceAbleObject;
    public TileManager TileManager;
    public GameManager GameManager;
    private SpriteRenderer spriteRenderer;
    private ObjectSpawner ObjectSpawner;
    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();
        TileManager = FindAnyObjectByType<TileManager>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        ObjectSpawner = FindAnyObjectByType<ObjectSpawner>();  
        if (spriteRenderer != null && ItemPlaceAbleObject != null)
        {
            spriteRenderer.sprite = ItemPlaceAbleObject.icon; 
        }
    }

    void Update()
    {

    }

    public void WaterPlant(int radius)
    {
        Vector3 objectMapPos = gameObject.transform.position - new Vector3(0.5f, 0.5f, 0);
        Vector3Int position = Vector3Int.RoundToInt(objectMapPos);

        int side = (radius - 1) / 2;
        for (int x = -side; x <= side; x++)
        {
            for (int y = -side; y <= side; y++)
            {
                Vector3Int offsetPosition = new Vector3Int(position.x + x, position.y + y, position.z);                
                TileManager.Watering(offsetPosition, false, false);
            }
        }
    }

    public void SpawnMineral(int radius)
    {
        Vector3 objectMapPos = gameObject.transform.position - new Vector3(0.5f, 0.5f, 0);
        Vector3Int position = Vector3Int.RoundToInt(objectMapPos);

        int side = (radius - 1) / 2;
        for (int x = -side; x <= side; x++)
        {
            for (int y = -side; y <= side; y++)
            {
                Vector3Int offsetPosition = new Vector3Int(position.x + x, position.y + y, position.z);
                ObjectSpawner.SpawnObject(offsetPosition, ItemPlaceAbleObject.PlaceAbleObjectData.SpawnedObjects, 50);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 objectMapPos = gameObject.transform.position - new Vector3(0.5f, 0.5f, 0);
        Vector3Int position = Vector3Int.RoundToInt(objectMapPos);
        TileManager.isPlaceObject = true;
        TileManager.HighlightTileUnderMouse(position, ItemPlaceAbleObject.PlaceAbleObjectData.Radius);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileManager.isPlaceObject = false;
        TileManager.DeHighLight();
    }
}

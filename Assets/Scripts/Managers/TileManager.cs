using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap interactableMap;
    public Tilemap highlightMap;
    public Tile hiddenInteractableTile;
    public Tile interactedTile;
    public GameObject PlantingSoil;
    public GameObject SeedPrefab;
    [SerializeField] private Tile highlightTile;
    public Vector3 mousePos;
    public Vector3Int cellPosition;
    private PlayerInventoryHolder playerInventoryHolder;
    private Vector3Int previousHighlightedCell;
    public Transform soilsParent;  
    public Transform seedsParent;

    void Start()
    {
        playerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        if (hiddenInteractableTile != null)
        {
            foreach (var position in interactableMap.cellBounds.allPositionsWithin)
            {
                if (interactableMap.HasTile(position))
                {
                    interactableMap.SetTile(position, hiddenInteractableTile);
                }
            }
        }
        previousHighlightedCell = new Vector3Int(-1, -1, -1);
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPosition = interactableMap.WorldToCell(mousePos);
        InventoryItemData holdItem = playerInventoryHolder.holdingItem;

        if (holdItem != null)
        {
            if (holdItem.itemType1 == ItemType.Interaction || holdItem.itemType2 == ItemType.Interaction)
            {
                if (Input.GetMouseButton(0) && GlobalVariables.CanUseInteractTools)
                {
                    Plow();
                }
                HighlightTileUnderMouse();
            }
            else if (holdItem.itemType1 == ItemType.Seed || holdItem.itemType2 == ItemType.Seed)
            {
                if (Input.GetMouseButton(0))
                {
                    PlantSeed();
                }
            }
            else
            {
                highlightMap.SetTile(previousHighlightedCell, null);
            }
        }
        else
        {
            highlightMap.SetTile(previousHighlightedCell, null);
        }
    }

    public void Plow()
    {
        if (IsInteractable(cellPosition))
        {
            SetInteracted(cellPosition);
        }
        else
        {
            Debug.Log("NO");
        }
    }

    public void PlantSeed()
    {
        if (IsSoilAtPosition(cellPosition))
        {
            Vector3 seedPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
            Instantiate(SeedPrefab, seedPosition, Quaternion.identity, seedsParent); 
            Debug.Log("Seed planted at: " + seedPosition);
        }
        else
        {
            Debug.Log("Cannot plant seed here!");
        }
    }

    public bool IsSoilAtPosition(Vector3Int position)
    {
        Vector3 worldPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

        if (hitCollider != null)
        {
            Soil soil = hitCollider.GetComponent<Soil>();
            if (soil != null)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile == null) return false;

        if (tile.name == "HiddenTile")
        {
            return true;
        }
        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);

        Vector3 truePos = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        Instantiate(PlantingSoil, truePos, Quaternion.identity, soilsParent);
    }

    public string GetTileName(Vector3Int position)
    {
        if (interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null) { return tile.name; }
        }
        return "no name";
    }

    private void HighlightTileUnderMouse()
    {
        if (cellPosition != previousHighlightedCell)
        {
            highlightMap.SetTile(previousHighlightedCell, null);
            previousHighlightedCell = cellPosition;
            highlightMap.SetTile(cellPosition, highlightTile);
        }
    }
}

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
    public InventoryDisplay inventoryDisplay;
    public ObjectsManager ObjectsManager;

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

        if (holdItem != null && GlobalVariables.CanAction)
        {
            if (holdItem.itemType1 == ItemType.Interaction || holdItem.itemType2 == ItemType.Interaction)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (holdItem.EquipableTag == EquipableTag.Shovel)
                    {
                        Plow();
                    }
                    else if (holdItem.EquipableTag == EquipableTag.WateringCan)
                    {
                        Watering();
                    }
                }
                HighlightTileUnderMouse();
            }
            else if (holdItem.itemType1 == ItemType.Seed || holdItem.itemType2 == ItemType.Seed)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlantSeed();
                }
                HighlightTileUnderMouse();

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

    public void Watering()
    {
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);

        if (hitCollider != null)
        {
            Soil soil = hitCollider.GetComponent<Soil>();
            if (soil != null)
            {
                soil.isWatered = true;
                soil.UpdateVisual();    
                ObjectsManager.SoilValues[worldPosition] = true; 
            }
        } else
        {
            Debug.Log("No soil to water here!");
        }
    }

    public void PlantSeed()
    {
        if (IsSoilAtPosition(cellPosition))
        {
            playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot].RemoveFromStack(1);
            inventoryDisplay.UpdateSlotStatic(playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot]);
            Vector3 seedPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y, 0);

            if (!ObjectsManager.SeedValues.Contains(seedPosition))
            {
                GameObject seedInstance = Instantiate(SeedPrefab, seedPosition, Quaternion.identity, seedsParent);
                Collider2D hitCollider = Physics2D.OverlapPoint(new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0));
                Soil soil = hitCollider.GetComponent<Soil>();
                Seed seedScript = seedInstance.GetComponent<Seed>();
                seedScript.thisSoil = soil;
                ObjectsManager.SeedValues.Add(seedPosition);
            } else
            {
                Debug.Log("Cannot plant seed here!");

            }
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
        ObjectsManager.SoilValues.Add(truePos, false);
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

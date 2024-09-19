using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Tilemap interactableMap;
    public Tilemap highlightMap;
    public Tilemap FarmSoilMap;
    public Tilemap FarmWaterMap;
    public Tilemap GrassBorderMap;

    public Tile hiddenInteractableTile;
    public Tile interactedTile;
    public Tile highlightTile;
    public TileBase SoilTile;
    public TileBase WateredSoilTile;
    public TileBase BorderTile;

    public GameObject PlantingSoil;
    public GameObject SeedPrefab;

    public Vector3 mousePos;
    public Vector3Int cellPosition;

    private PlayerInventoryHolder playerInventoryHolder;
    private Vector3Int previousHighlightedCell;
    public InventoryDisplay inventoryDisplay;
    public ObjectsManager ObjectsManager;
    private GameManager gameManager;

    public Transform soilsParent;  
    public Transform seedsParent;


    void Start()
    {
        interactableMap.gameObject.SetActive(true);
        playerInventoryHolder = FindAnyObjectByType<PlayerInventoryHolder>();
        gameManager = FindObjectOfType<GameManager>();
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
                    } else if (holdItem.EquipableTag == EquipableTag.Hoe)
                    {
                        Harvest();
                    }
                    else if (holdItem.EquipableTag == EquipableTag.Axe)
                    {
                        ChopWood();
                    }
                    else if (holdItem.EquipableTag == EquipableTag.Pickaxe)
                    {
                        Mine();
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

    private void Mine()
    {
        int soilLayerMask = LayerMask.GetMask("Object");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

        if (hitCollider != null)
        {
            Object objectHit = hitCollider.GetComponent<Object>();
            if (objectHit.ObjectData.ObjectType == ObjectType.Stone && gameManager.ActionAble(3))
            {
                objectHit.ModifyHealth(2);
                gameManager.CurrentEnergy -= 3;

                if (objectHit.ObjectHealth <= 0)
                {
                    Destroy(objectHit.gameObject);
                    playerInventoryHolder.PrimaryInventorySystem.AddToInventory(objectHit.ObjectData.DropItems[0], objectHit.ObjectData.DropQuantity);

                }
            }
        } else
        {
        }
    }

    private void ChopWood()
    {
        int soilLayerMask = LayerMask.GetMask("Object");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

        if (hitCollider != null)
        {
            Object objectHit = hitCollider.GetComponent<Object>();
            if (objectHit.ObjectData.ObjectType == ObjectType.Wood && gameManager.ActionAble(3))
            {
                objectHit.ModifyHealth(2);
                gameManager.CurrentEnergy -= 3;

                if (objectHit.ObjectHealth <= 0)
                {
                    Destroy(objectHit.gameObject);
                    playerInventoryHolder.PrimaryInventorySystem.AddToInventory(objectHit.ObjectData.DropItems[0], objectHit.ObjectData.DropQuantity);
                }
            }
        }
        else
        {
        }
    }

    public void Plow()
    {
        if (IsInteractable(cellPosition) && gameManager.ActionAble(10))
        {
            SetInteracted(cellPosition);
            gameManager.ConsumeEnergy(10);
        }
        else
        {
            Debug.Log("NO");
        }
    }

    public void Watering()
    {
        int soilLayerMask = LayerMask.GetMask("Soil");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

        if (hitCollider != null)
        {
            Soil soil = hitCollider.GetComponent<Soil>();
            if (soil != null && gameManager.ActionAble(10))
            {
                soil.isWatered = true;
                FarmWaterMap.SetTile(cellPosition,WateredSoilTile);   
                

                if (ObjectsManager.SoilValues[worldPosition] == false)
                {
                    ObjectsManager.SoilValues[worldPosition] = true;
                    gameManager.ConsumeEnergy(10);

                }

            }
        } else
        {
            Debug.Log("No soil to water here!");
        }
    }

    public void Harvest()
    {
        int seedLayerMask = LayerMask.GetMask("Seed");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, seedLayerMask);

        if (hitCollider != null)
        {
            Seed seed = hitCollider.GetComponent<Seed>();
            if (seed != null)
            {
                if (seed.Harvestable && gameManager.ActionAble(10))
                {
                    int dropAmount = GetRandomDropAmount();
                    playerInventoryHolder.PrimaryInventorySystem.AddToInventory(seed.SeedData.SeedProduct, dropAmount);
                    ObjectsManager.SeedValues.Remove(seed.position);
                    gameManager.ConsumeEnergy(10);

                    Destroy(seed.gameObject);
                }
            }
        }
    }

    private int GetRandomDropAmount()
    {
        int[] dropChances = { 3, 3, 3, 4, 4, 4, 5, 5, 6, 7 };
        int randomIndex = Random.Range(0, dropChances.Length);
        return dropChances[randomIndex];
    }


    public void PlantSeed()
    {
        if (IsSoilAtPosition(cellPosition))
        {
            
            Vector3 seedPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y, 0);


            if (ObjectsManager.SeedValues.ContainsKey(seedPosition))
            {
                Debug.Log("A seed is already planted here!");
                return;
            }
            playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot].RemoveFromStack(1);
            inventoryDisplay.UpdateSlotStatic(playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot]);
            GameObject seedInstance = Instantiate(SeedPrefab, seedPosition, Quaternion.identity, seedsParent);
            int soilLayerMask = LayerMask.GetMask("Soil");
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0), soilLayerMask);
            Soil soil = hitCollider.GetComponent<Soil>();
            Seed seedScript = seedInstance.GetComponent<Seed>();
            seedScript.thisSoil = soil;
            seedScript.position = seedPosition;
            ObjectsManager.SeedValues.Add(seedPosition, 0);
            
        }
        else
        {
            Debug.Log("Cannot plant seed here!");
        }
    }

    public bool IsSoilAtPosition(Vector3Int position)
    {
        Vector3 worldPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        int soilLayerMask = LayerMask.GetMask("Soil");
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

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

        Vector3 worldPosition = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        int soilLayerMask = LayerMask.GetMask("Object");
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

        if (hitCollider == null)
        {
            if (tile.name == "HiddenTile")
            {
                return true;
            }
        }

        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
        FarmSoilMap.SetTile(position, SoilTile);

        Vector3 truePos = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        Instantiate(PlantingSoil, truePos, Quaternion.identity, soilsParent);
        ObjectsManager.SoilValues.Add(truePos, false);
    }

    private void PlaceBordersAround(Vector3Int centerPosition)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int borderPosition = new Vector3Int(centerPosition.x + x, centerPosition.y + y, centerPosition.z);

                GrassBorderMap.SetTile(borderPosition, BorderTile);
            }
        }
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

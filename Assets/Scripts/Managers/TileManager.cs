using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap interactableMap;
    public Tilemap highlightMap;
    public Tilemap FarmSoilMap;
    public Tilemap FarmWaterMap;

    [Header("Tiles")]
    public Tile hiddenInteractableTile;
    public Tile interactedTile;
    public Tile highlightTile;
    public TileBase SoilTile;
    public TileBase WateredSoilTile;

    [Header("Misc")]
    public GameObject PlantingSoil;
    public GameObject SeedPrefab;

    public Vector3 mousePos;
    public Vector3Int cellPosition;

    private PlayerInventoryHolder playerInventoryHolder;
    private Vector3Int previousHighlightedCell;
    public InventoryDisplay inventoryDisplay;
    public ObjectsManager ObjectsManager;
    private GameManager gameManager;
    private GameObject currentAnimation;

    [Header("Object Parents")]
    public Transform dropItems;
    public Transform soilsParent;  
    public Transform seedsParent;

    [Header("Animations")]
    public GameObject chopAnimationPrefab;
    public GameObject mineAnimationPrefab;
    public GameObject waterAnimationPrefab;
    public GameObject plowAnimationPrefab;
    public GameObject digAnimationPrefab;
    public GameObject dropItemPrefab;


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
                if (Input.GetMouseButton(0))
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
        CheckForItemPickup();
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
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);

                SpawnAnimation(mineAnimationPrefab, worldPosition1);
                StartCoroutine(ExecuteAfterDelay(0.4f, objectHit));
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
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);

                SpawnAnimation(chopAnimationPrefab, worldPosition1);
                StartCoroutine(ExecuteAfterDelay(0.4f, objectHit));
            }
        }
        else
        {
        }
    }
    private void SpawnAnimation(GameObject animationPrefab, Vector3 position)
    {
        if (currentAnimation != null)
        {
            Destroy(currentAnimation);
        }
        currentAnimation = Instantiate(animationPrefab, position, Quaternion.identity);
        Destroy(currentAnimation, 0.5f);
    }
    private IEnumerator ExecuteAfterDelay(float delay, Object objectHit)
    {
        yield return new WaitForSeconds(delay);

        if (objectHit != null)  
        {
            DestroyObject(objectHit);
        }
    }

    private void DestroyObject(Object objectHit)
    {
        if (objectHit == null) return;  

        objectHit.ModifyHealth(2);
        gameManager.CurrentEnergy -= 3;

        if (objectHit.ObjectHealth <= 0)
        {
            DropItem(objectHit);
            Destroy(objectHit.gameObject);
        }
    }


    public void DropItem(Object objectHit)
    {
        GameObject instantiatedAnimation = Instantiate(dropItemPrefab, objectHit.ObjectPosition, Quaternion.identity,dropItems);
        DropItemData dropItemData = instantiatedAnimation.GetComponent<DropItemData>();

        if (dropItemData != null)
        {
            dropItemData.InitializeDrop(objectHit.ObjectData.DropItems[0]);
            StartCoroutine(EnablePickupAfterDelay(instantiatedAnimation, 1f)); 
        }
    }

    public void DropItem(InventoryItemData itemDrop, Vector3 position)
    {
        GameObject instantiatedAnimation = Instantiate(dropItemPrefab, position, Quaternion.identity, dropItems);
        DropItemData dropItemData = instantiatedAnimation.GetComponent<DropItemData>();

        if (dropItemData != null)
        {
            dropItemData.InitializeDrop(itemDrop);
            StartCoroutine(EnablePickupAfterDelay(instantiatedAnimation, 1f));
        }
    }

    private IEnumerator EnablePickupAfterDelay(GameObject dropItem, float delay)
    {
        yield return new WaitForSeconds(delay);
        dropItem.GetComponent<DropItemData>().CanBePickedUp = true; 
    }

    private bool isPickingUp = false;

    private void CheckForItemPickup()
    {
        if (isPickingUp) return; // Exit if a pickup is already in progress

        float pickupRange = 1f;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Collider2D[] nearbyItems = Physics2D.OverlapCircleAll(mousePosition, pickupRange);

        foreach (var item in nearbyItems)
        {
            DropItemData dropItemData = item.GetComponent<DropItemData>();
            if (dropItemData != null && dropItemData.CanBePickedUp)
            {
                isPickingUp = true;
                StartCoroutine(MoveItemTowardsMouse(dropItemData.gameObject, mousePosition));
                break;
            }
        }
    }

    private IEnumerator MoveItemTowardsMouse(GameObject dropItem, Vector3 targetPosition)
    {
        DropItemData dropItemData = dropItem.GetComponent<DropItemData>();
        

        float speed = 10f;

        while (dropItem != null && Vector3.Distance(dropItem.transform.position, targetPosition) > 0.1f)
        {
            dropItem.transform.position = Vector3.MoveTowards(dropItem.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        if (dropItem != null)
        {
            AddToInventory(dropItem);
        }

        isPickingUp = false; // Reset the flag after pickup
    }


    private IEnumerator AddToParentAfterDelay(float delay, Vector3Int pos)
    {
        yield return new WaitForSeconds(delay);
        SetInteracted(pos);
    }


    private void AddToInventory(GameObject dropItem)
    {
        DropItemData dropItemData = dropItem.GetComponent<DropItemData>();

        if (dropItemData != null)
        {

            playerInventoryHolder.AddToHotBar(dropItemData.inventoryItemData, dropItemData.inventoryItemData.GetTotalDropCount());
            Destroy(dropItem);

        }
    }

    public void Plow()
    {
        if (IsInteractable(cellPosition) && gameManager.ActionAble(8))
        {
            Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);

            SpawnAnimation(digAnimationPrefab, worldPosition1);
            StartCoroutine(AddToParentAfterDelay(0.2f, cellPosition));
        }
        else
        {
            Vector3 seedPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y, 0);
            if (ObjectsManager.SeedValues.ContainsKey(seedPosition))
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);
                SpawnAnimation(digAnimationPrefab, worldPosition1);

                int seedLayerMask = LayerMask.GetMask("Seed");
                Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
                Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, seedLayerMask);

                if (hitCollider != null)
                {
                    Seed seed = hitCollider.GetComponent<Seed>();
                    if (seed != null)
                    {
                        seed.isDestroyed = true;
                        ObjectsManager.SeedValues.Remove(seedPosition);
                        Destroy(seed.gameObject);
                    }
                }

            }
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
            if (soil != null && gameManager.ActionAble(4))
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 1f, 0);

                SpawnAnimation(waterAnimationPrefab, worldPosition1);
                StartCoroutine(WaterDelay(worldPosition, cellPosition, soil));

            }
        } else
        {
            Debug.Log("No soil to water here!");
        }
    }

    public IEnumerator WaterDelay(Vector3 worldPosition, Vector3Int cellPosition, Soil soil)
    {
        yield return new WaitForSeconds(0.2f);
        WaterPlant(worldPosition, cellPosition, soil);
    }

    public void WaterPlant(Vector3 worldPosition, Vector3Int cellPosition, Soil soil)
    {
        soil.isWatered = true;
        FarmWaterMap.SetTile(cellPosition, WateredSoilTile);


        if (ObjectsManager.SoilValues[worldPosition] == false)
        {
            ObjectsManager.SoilValues[worldPosition] = true;
            gameManager.ConsumeEnergy(4);

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
                if (seed.Harvestable && gameManager.ActionAble(1))
                {
                    worldPosition.x -= 0.5f;
                    StartCoroutine(HarvestDelay(seed));
                    SpawnAnimation(plowAnimationPrefab, worldPosition);
                }
            }
        }
    }
    public IEnumerator HarvestDelay(Seed seed)
    {
        yield return new WaitForSeconds(0.2f);
        HarvestPlant(seed);
    }

    public void HarvestPlant(Seed seed)
    {
        ObjectsManager.SeedValues.Remove(seed.position);
        gameManager.ConsumeEnergy(1);

        DropItem(seed.SeedData.SeedProduct, seed.position);
        Destroy(seed.gameObject);
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
            
            GameObject seedInstance = Instantiate(playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot].ItemData.Prefab, seedPosition, Quaternion.identity, seedsParent);
            int soilLayerMask = LayerMask.GetMask("Soil");
            Collider2D hitCollider = Physics2D.OverlapPoint(new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0), soilLayerMask);
            Soil soil = hitCollider.GetComponent<Soil>();
            Seed seedScript = seedInstance.GetComponent<Seed>();
            seedScript.thisSoil = soil;
            seedScript.position = seedPosition;
            ObjectsManager.SeedValues.Add(seedPosition, 0);
            playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot].RemoveFromStack(1);
            inventoryDisplay.UpdateSlotStatic(playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot]);

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
        gameManager.ConsumeEnergy(8);
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

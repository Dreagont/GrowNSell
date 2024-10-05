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
    public TileBase GrassTile;

    [Header("Misc")]
    public GameObject PlantingSoil;
    public InventoryItemData SoilDrop;

    private Vector3 mousePos;
    public Vector3Int cellPosition;

    public PlayerInventoryHolder playerInventoryHolder;
    private Vector3Int previousHighlightedCell;
    public InventoryDisplay inventoryDisplay;
    public ObjectsManager ObjectsManager;
    private GameManager gameManager;
    private GameObject currentAnimation;

    [Header("Object Parents")]
    public Transform dropItems;
    public Transform soilsParent;
    public Transform seedsParent;
    public Transform HighLightObjectParent;
    public Transform PlaceItemParent;

    public GameObject dropItemPrefab;

    public GameObject HighLightObject;
    public GameObject PlaceAbleObject;
    private ExperientManager experientManager;
    public bool isPlaceObject = false;
    private ObjectSpawner objectSpawner;
    private InventoryItemData holdItem;

    void Start()
    {
        objectSpawner = FindAnyObjectByType<ObjectSpawner>();
        experientManager = FindAnyObjectByType<ExperientManager>();
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
        holdItem = playerInventoryHolder.holdingItem;

        if (holdItem != null && GlobalVariables.CanAction)
        {
            if (holdItem.itemType1 == ItemType.Interaction || holdItem.itemType2 == ItemType.Interaction)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (holdItem.EquipableTag == EquipableTag.Hoe)
                    {
                        Plow(holdItem.ToolData.ToolEnergy, holdItem.ToolData.ToolAnimation, holdItem.ToolData.ToolRadius);
                        Harvest(holdItem.ToolData.ToolEnergy, holdItem.ToolData.ToolAnimation);
                    }
                    else if (holdItem.EquipableTag == EquipableTag.Axe)
                    {
                        ChopWood(holdItem.ToolData.ToolEnergy, holdItem.ToolData.ToolAnimation, holdItem.ToolData.ToolTier);
                    }
                    else if (holdItem.EquipableTag == EquipableTag.Pickaxe)
                    {
                        Mine(holdItem.ToolData.ToolEnergy, holdItem.ToolData.ToolAnimation, holdItem.ToolData.ToolTier);
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (holdItem.EquipableTag == EquipableTag.Hoe)
                    {
                        DestroyCrop(holdItem.ToolData.ToolAnimation);
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    if (holdItem.EquipableTag == EquipableTag.WateringCan)
                    {
                        Watering(cellPosition, true, true, holdItem.ToolData.ToolEnergy, holdItem.ToolData.ToolAnimation, holdItem.ToolData.ToolRadius);
                    }
                }
                HighlightTileUnderMouse(cellPosition, holdItem.ToolData.ToolRadius);
            }
            else if (holdItem.itemType1 == ItemType.Seed || holdItem.itemType2 == ItemType.Seed)
            {
                if (Input.GetMouseButton(0))
                {
                    PlantSeed();
                }
                HighlightTileUnderMouse(cellPosition, 1);

            } else if (holdItem.itemType1 == ItemType.PlaceAble || holdItem.itemType2 == ItemType.PlaceAble)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    objectSpawner.PlaceItem(holdItem,cellPosition,PlaceAbleObject,PlaceItemParent);
                    
                }
                HighlightTileUnderMouse(cellPosition, holdItem.PlaceAbleObjectData.Radius);
            } else if (holdItem.itemType1 == ItemType.ConsumeAble || holdItem.itemType2 == ItemType.ConsumeAble)
            {
                if (Input.GetMouseButtonDown(0))
                { 
                    UseItem(holdItem);

                }
                HighlightTileUnderMouse(cellPosition, 1);

            }
            else
            {
                HighlightTileUnderMouse(cellPosition, 1);
            }
        }
        else
        {
            HighlightTileUnderMouse(cellPosition, 1);

        }
        CheckForItemPickup();
    }

    public void UseItem(InventoryItemData holdItem)
    {
        gameManager.EnergyManager.CurrentEnergy += holdItem.EnergyRecover;
        playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot].RemoveFromStack(1);
        inventoryDisplay.UpdateSlotStatic(playerInventoryHolder.PrimaryInventorySystem.InventorySlots[playerInventoryHolder.SelectedSlot]);

    }

    private void Mine(int PickaxeEnergy, GameObject mineAnimationPrefab, int dropMultiply)
    {
        int objectLayerMask = LayerMask.GetMask("Object");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, objectLayerMask);

        if (hitCollider != null)
        {
            Object objectHit = hitCollider.GetComponent<Object>();
            PlaceAbleObject placeAbleObject = hitCollider.GetComponent<PlaceAbleObject>();

            int energyMulti = objectHit.ObjectHealth / (2 + gameManager.BuffManager.ToolDamageBuff);

            if (objectHit.ObjectData.ObjectType == ObjectType.Stone && gameManager.EnergyManager.ActionAble(PickaxeEnergy * energyMulti))
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);
                isPlaceObject = false;
                DeHighLight();
                objectSpawner.ObjectPosition.Remove(objectHit.ObjectPosition);
                SpawnAnimation(mineAnimationPrefab, worldPosition1);
                StartCoroutine(ExecuteAfterDelay(0.4f, objectHit, PickaxeEnergy * energyMulti, energyMulti, dropMultiply, placeAbleObject == null));
            }
        }
        else
        {
        }
    }

    private void ChopWood(int AxeEnergy, GameObject chopAnimationPrefab, int dropMultiply)
    {
        int objectLayerMask = LayerMask.GetMask("Object");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, objectLayerMask);

        if (hitCollider != null)
        {
            Object objectHit = hitCollider.GetComponent<Object>();
            PlaceAbleObject placeAbleObject = hitCollider.GetComponent<PlaceAbleObject>();
            int energyMulti = objectHit.ObjectHealth / (2 + gameManager.BuffManager.ToolDamageBuff);

            if (objectHit.ObjectData.ObjectType == ObjectType.Wood && gameManager.EnergyManager.ActionAble(AxeEnergy * energyMulti))
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);
                objectSpawner.ObjectPosition.Remove(objectHit.ObjectPosition);
                SpawnAnimation(chopAnimationPrefab, worldPosition1);
                StartCoroutine(ExecuteAfterDelay(0.4f, objectHit, AxeEnergy * energyMulti, energyMulti, dropMultiply, placeAbleObject == null));
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
    private IEnumerator ExecuteAfterDelay(float delay, Object objectHit, int energy, int energyMulti, int dropMultiply, bool isMaterial)
    {
        yield return new WaitForSeconds(delay);

        if (objectHit != null)
        {
            DestroyObject(objectHit, energy, energyMulti, dropMultiply, isMaterial);
        }
    }

    private void DestroyObject(Object objectHit, int energy, int energyMulti, int dropMultiply, bool isMaterial)
    {
        if (objectHit == null) return;

        objectHit.ModifyHealthFull();
        gameManager.EnergyManager.ConsumeEnergyTools(energy, energyMulti);

        if (objectHit.ObjectHealth <= 0)
        {
            Vector3 postion = objectHit.ObjectPosition - new Vector3(0.5f, 0.5f, 0);
            Vector3Int IPosition = Vector3Int.RoundToInt(postion);

            DropItem(objectHit, dropMultiply, isMaterial);
            Destroy(objectHit.gameObject);
            if (FarmSoilMap.GetTile(IPosition).name != "RandomGrass")
            {
                FarmSoilMap.SetTile(IPosition, GrassTile);
            }
        }
    }


    public void DropItem(Object objectHit,int dropMultiply, bool isMaterial)
    {
        {
            foreach (var item in objectHit.ObjectData.DropItems)
            {
                GameObject instantiatedAnimation = Instantiate(dropItemPrefab, objectHit.ObjectPosition + new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f),0), Quaternion.identity, dropItems);
                DropItemData dropItemData = instantiatedAnimation.GetComponent<DropItemData>();

                if (isMaterial)
                {
                    dropItemData.InitializeDrop(item, (objectHit.ObjectData.DropQuantity + gameManager.BuffManager.MaterialBonus) * dropMultiply);
                } else
                {
                    dropItemData.InitializeDrop(item, 1);
                }
                StartCoroutine(EnablePickupAfterDelay(instantiatedAnimation, 1f));
            }
        }
    }

    public void DropItem(InventoryItemData itemDrop, Vector3 position, bool isDouble)
    {
        if (itemDrop != null)
        {
            GameObject instantiatedAnimation = Instantiate(dropItemPrefab, position, Quaternion.identity, dropItems);
            DropItemData dropItemData = instantiatedAnimation.GetComponent<DropItemData>();

            if (dropItemData != null)
            {
                if (isDouble)
                {
                    dropItemData.InitializeDrop(itemDrop, true);

                }
                else
                {
                    dropItemData.InitializeDrop(itemDrop, 1);
                }

                StartCoroutine(EnablePickupAfterDelay(instantiatedAnimation, 1f));
            }
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


    private IEnumerator AddToParentAfterDelay(float delay, Vector3Int pos,int HoePlowEnergy)
    {
        yield return new WaitForSeconds(delay);
        SetInteracted(pos, HoePlowEnergy);
    }


    private void AddToInventory(GameObject dropItem)
    {
        DropItemData dropItemData = dropItem.GetComponent<DropItemData>();

        if (dropItemData != null)
        {

            playerInventoryHolder.AddToHotBar(dropItemData.inventoryItemData, dropItemData.dropCount);

            Destroy(dropItem);
        }
    }

    public void Plow(int HoePlowEnergy, GameObject plowAnimationPrefab, int radius)
    {
        bool isEnergy = true;

        if (IsInteractable(cellPosition) && gameManager.EnergyManager.ActionAble(8))
        { 
            if (radius == 1)
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);

                SpawnAnimation(plowAnimationPrefab, worldPosition1);
                StartCoroutine(AddToParentAfterDelay(0.2f, cellPosition, HoePlowEnergy));
            }
            else
            {
                Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);

                SpawnAnimation(plowAnimationPrefab, worldPosition1);

                int side = (radius - 1) / 2;
                for (int x = -side; x <= side; x++)
                {
                    for (int y = -side; y <= side; y++)
                    {
                        Vector3Int offsetPosition = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);
                        if (!IsInteractable(offsetPosition))
                        {
                            continue;
                        }
                        if (FarmSoilMap.GetTile(offsetPosition).name == "Water")
                        {
                            continue;
                        }
                        if (isEnergy)
                        {
                            StartCoroutine(AddToParentAfterDelay(0.2f, offsetPosition, HoePlowEnergy));
                            isEnergy = false;
                        }
                        else
                        {
                            StartCoroutine(AddToParentAfterDelay(0.2f, offsetPosition, 0));
                        }
                    }
                }
            }
        } 
    }

    public void DestroyCrop(GameObject plowAnimationPrefab)
    {
        Vector3 seedPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y, 0);
        if (ObjectsManager.SeedValues.ContainsKey(seedPosition))
        {
            Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 0.5f, 0);
            SpawnAnimation(plowAnimationPrefab, worldPosition1);

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
                    StartCoroutine(DelayDestroy(seed.gameObject));
                }
            }

        }
    }
    public IEnumerator DelayDestroy(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    public void Watering(Vector3Int cellPosition, bool spwanAnimation, bool consumeEnergy,int WaterEnergy, GameObject waterAnimationPrefab, int radius)
    {
        int soilLayerMask = LayerMask.GetMask("Soil");

        if (radius == 1)
        {
            Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
            Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);

            if (hitCollider != null)
            {
                Soil soil = hitCollider.GetComponent<Soil>();
                if (soil != null && gameManager.EnergyManager.ActionAble(4))
                {
                    if (spwanAnimation)
                    {
                        Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 1f, 0);
                        SpawnAnimation(waterAnimationPrefab, worldPosition1);
                    }
                    StartCoroutine(WaterDelay(worldPosition, cellPosition, soil, consumeEnergy, WaterEnergy));

                }
            }
        }
        else
        {
            bool isEnergy = true;
            int side = (radius - 1) / 2;
            for (int x = -side; x <= side; x++)
            {
                for (int y = -side; y <= side; y++)
                {
                    Vector3Int offsetPosition = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);
                    Vector3 worldPosition = new Vector3(offsetPosition.x + 0.5f, offsetPosition.y + 0.5f, 0);
                    Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, soilLayerMask);
                    if (spwanAnimation)
                    {
                        Vector3 worldPosition1 = new Vector3(cellPosition.x + 0.0f, cellPosition.y + 1f, 0);
                        SpawnAnimation(waterAnimationPrefab, worldPosition1);
                    }
                    if (hitCollider != null)
                    {
                        Soil soil = hitCollider.GetComponent<Soil>();
                        if (isEnergy)
                        {
                            if (soil != null && gameManager.EnergyManager.ActionAble(WaterEnergy))
                            {
                                StartCoroutine(WaterDelay(worldPosition, offsetPosition, soil, consumeEnergy, WaterEnergy));
                                isEnergy = false;
                            }
                        }
                        else
                        {
                            if (soil != null && gameManager.EnergyManager.ActionAble(0))
                            {
                                StartCoroutine(WaterDelay(worldPosition, offsetPosition, soil, consumeEnergy, 0));
                            }
                        }
                    }
                }
            }
        }
    }

    public IEnumerator WaterDelay(Vector3 worldPosition, Vector3Int cellPosition, Soil soil, bool consumeEnergy, int WaterEnergy)
    {
        yield return new WaitForSeconds(0.2f);
        WaterPlant(worldPosition, cellPosition, soil, consumeEnergy, WaterEnergy);
    }

    public void WaterPlant(Vector3 worldPosition, Vector3Int cellPosition, Soil soil, bool consumeEnergy, int WaterEnergy)
    {
        soil.isWatered = true;
        FarmWaterMap.SetTile(cellPosition, WateredSoilTile);


        if (ObjectsManager.SoilValues[worldPosition] == false)
        {
            ObjectsManager.SoilValues[worldPosition] = true;
            if (consumeEnergy)
            {
                gameManager.EnergyManager.ConsumeEnergyTools(WaterEnergy, 1);
            }
        }
    }

    public void Harvest(int HoeHarvestEnergy, GameObject plowAnimationPrefab)
    {
        int seedLayerMask = LayerMask.GetMask("Seed");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition, seedLayerMask);

        if (hitCollider != null)
        {
            Seed seed = hitCollider.GetComponent<Seed>();
            if (seed != null)
            {
                if (seed.Harvestable && gameManager.EnergyManager.ActionAble(1))
                {
                    worldPosition.x -= 0.5f;
                    StartCoroutine(HarvestDelay(seed, HoeHarvestEnergy));
                    SpawnAnimation(plowAnimationPrefab, worldPosition);
                }
            }
        }
    }
    public IEnumerator HarvestDelay(Seed seed, int HoeHarvestEnergy)
    {
        yield return new WaitForSeconds(0.2f);
        HarvestPlant(seed, HoeHarvestEnergy);
    }

    public void HarvestPlant(Seed seed, int HoeHarvestEnergy)
    {

        ObjectsManager.SeedValues.Remove(seed.position);
        ObjectsManager.Seeds.Remove(seed);
        gameManager.EnergyManager.ConsumeEnergyTools(HoeHarvestEnergy, 1);
        seed.thisSoil.isSeeded = false;

        DestroyFarmLand(seed, seed.SeedData.NotDestroy + gameManager.BuffManager.NotDestroyFarmland);

        DropItem(seed.SeedData.SeedProduct, seed.position, gameManager.IsSuccess(seed.SeedData.DoubleDrop + gameManager.BuffManager.DoubleDropChance));

        Destroy(seed.gameObject);
    }

    public void DestroyFarmLand(Seed seed, float chance)
    {
        if (gameManager.IsSuccess(chance))
        {
            return;
        }
        else
        {
            setTileGrass(seed.position);
            Destroy(seed.thisSoil.gameObject);
        }
    }

    public void setTileGrass(Vector3 position)
    {
        Vector3Int grassPos = new Vector3Int((int)(position.x - 0.5f), (int)(position.y), 0);
        FarmSoilMap.SetTile(grassPos, GrassTile);
        interactableMap.SetTile(grassPos, hiddenInteractableTile);
        FarmWaterMap.SetTile(grassPos,null);
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
            seedScript.thisSoil.isSeeded = true;
            ObjectsManager.SeedValues.Add(seedPosition, 0);
            ObjectsManager.Seeds.Add(seedScript);
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
        Vector3 truePos = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);

        if (ObjectsManager.SoilValues.ContainsKey(truePos) == true )
        {
            return false;
        }

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

    public void SetInteracted(Vector3Int position, int HoePlowEnergy)
    {
        Vector3 truePos = new Vector3(position.x + 0.5f, position.y + 0.5f, 0);
        if (ObjectsManager.SoilValues.ContainsKey(truePos) == true)
        {
            return;
        }
        interactableMap.SetTile(position, interactedTile);
        FarmSoilMap.SetTile(position, SoilTile);

        GameObject soilObject = Instantiate(PlantingSoil, truePos, Quaternion.identity, soilsParent);
        Soil soii = soilObject.GetComponent<Soil>();
        gameManager.EnergyManager.ConsumeEnergyTools(HoePlowEnergy, 1);
        DropItem(gameManager.DropsManager.DropPlowItem(gameManager.BuffManager.PlowDropChance), truePos, false);
        DropItem(SoilDrop, truePos, false);
        experientManager.SpawnExp(1, truePos);
        ObjectsManager.SoilValues.Add(truePos, false);
        ObjectsManager.Soils.Add(soii);
    }

    public void HighlightTileUnderMouse(Vector3Int cellPosition, int radius)
    {
        if (cellPosition != previousHighlightedCell)
        {
            if (radius > 1)
            {
                DeHighLight();
                int side = (radius - 1) / 2;
                for (int x = -side; x <= side; x++)
                {
                    for (int y = -side; y <= side; y++)
                    {
                        Vector3Int offsetPosition = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);
                        Vector3 highPos = new Vector3(cellPosition.x + x + 0.5f, cellPosition.y + y + 0.5f, cellPosition.z);
                        if (FarmSoilMap.GetTile(offsetPosition).name == "Water")
                        {
                            continue;
                        }
                        Instantiate(HighLightObject, highPos, Quaternion.identity, HighLightObjectParent);
                    }
                }
                previousHighlightedCell = cellPosition;
            }
            else if (isPlaceObject == false)
            {
                if (FarmSoilMap.GetTile(cellPosition) != null && FarmSoilMap.GetTile(cellPosition).name != "Water")
                {
                    DeHighLight();
                    Vector3 highPos = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, cellPosition.z);
                    Instantiate(HighLightObject, highPos, Quaternion.identity, HighLightObjectParent);
                    previousHighlightedCell = cellPosition;
                }
                else
                {
                    DeHighLight();
                }

            }
        }
        else
        {
            //DeHighLight();
        }


    }

    public void DeHighLight()
    {
            foreach (Transform child in HighLightObjectParent)
            {
                Destroy(child.gameObject);
            }
        
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    public Tilemap Ground;
    public List<GameObject> BaseWoodObjects = new List<GameObject>();
    public List<GameObject> BaseStoneObjects = new List<GameObject>();
    public Transform ObjectsParent;
    public List<Vector3> ObjectPosition = new List<Vector3>();
    public int totalscore = 0;
    private TileManager TileManager;
    private void Start()
    {
        TileManager = FindAnyObjectByType<TileManager>();   
    }
    public void SpawnObject()
    {
        int objectLayer = LayerMask.NameToLayer("Object");

        foreach (var position in Ground.cellBounds.allPositionsWithin)
        {
            if (Ground.HasTile(position))
            {
                if (Ground.GetTile(position).name == "RandomGrass")
                {
                    Vector3 truePos = Ground.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

                    if (Random.Range(1, 4) < 3)
                    {
                        int objectIndex = Random.Range(0, BaseWoodObjects.Count);
                        GameObject spawnedObject = Instantiate(BaseWoodObjects[objectIndex], truePos, Quaternion.identity, ObjectsParent);
                        ObjectPosition.Add(truePos);
                        Object objectSpwaned = spawnedObject.GetComponent<Object>();
                        objectSpwaned.ObjectPosition = truePos;
                        SetLayerRecursively(spawnedObject, objectLayer);
                    }
                } 
                
            }
        }
    }

    public void SpawnObject(Vector3Int position)
    {
        if (Random.Range(1, 100) < 50)
        {
            int objectLayer = LayerMask.NameToLayer("Object");
            Vector3 truePos = Ground.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            truePos.x = RoundToNearestHalf(truePos.x);
            truePos.y = RoundToNearestHalf(truePos.y);
            bool spawnWood = Random.Range(1, 100) < 50;

            if (spawnWood)
            {
                SpawnObjectFromList(BaseWoodObjects, truePos, objectLayer);
            }
            else if (!spawnWood)
            {
                SpawnObjectFromList(BaseStoneObjects, truePos, objectLayer);
            }
        }
        
    }

    public void SpawnObject(Vector3Int position, List<GameObject> SpawnList, int chance)
    {
        if (Random.Range(1, 100) < chance)
        {
            int objectLayer = LayerMask.NameToLayer("Object");
            Vector3 truePos = Ground.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
            truePos.x = RoundToNearestHalf(truePos.x);
            truePos.y = RoundToNearestHalf(truePos.y);

            if (ObjectPosition.Contains(truePos))
            {
                return;
            } else
            {
                SpawnObjectFromList(SpawnList, truePos, objectLayer);

            }
        }
    }

    private void SpawnObjectFromList(List<GameObject> baseObjects, Vector3 position, int layer)
    {
        
        position.x = RoundToNearestHalf(position.x);
        position.y = RoundToNearestHalf(position.y);
        List<GameObject> weightedObjects = new List<GameObject>();

        foreach (var baseObject in baseObjects)
        {
            Object objectComponent = baseObject.GetComponent<Object>();
            for (int i = 0; i < objectComponent.ObjectData.ObjectScore; i++)
            {
                weightedObjects.Add(baseObject);
            }
        }

        int objectIndex = Random.Range(0, weightedObjects.Count);
        GameObject spawnedObject = Instantiate(weightedObjects[objectIndex], position, Quaternion.identity, ObjectsParent);
        ObjectPosition.Add(position);
        Object objectSpawned = spawnedObject.GetComponent<Object>();
        objectSpawned.ObjectPosition = position;

        SetLayerRecursively(spawnedObject, layer);
    }


    public void PlaceItem(InventoryItemData holdItem, Vector3Int cellPosition, GameObject PlaceAbleObject, Transform PlaceItemParent)
    {
        int objectLayer = LayerMask.NameToLayer("Object");
        Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);

        if (ObjectPosition.Contains(worldPosition))
        {
            return;
        } else
        {
            GameObject placeItem = Instantiate(PlaceAbleObject, worldPosition, Quaternion.identity, PlaceItemParent);
            ObjectPosition.Add(worldPosition);
            PlaceAbleObject placeAble = placeItem.GetComponent<PlaceAbleObject>();
            Object objectPlace = placeItem.GetComponent<Object>();
            objectPlace.ObjectPosition = worldPosition;
            placeAble.ItemPlaceAbleObject = holdItem;
            objectPlace.ObjectData = holdItem.PlaceAbleObjectData.PlaceItemObjectData;
            if (placeAble.ItemPlaceAbleObject.PlaceAbleObjectData.Undertile !=  null)
            {
                TileManager.FarmSoilMap.SetTile(cellPosition, placeAble.ItemPlaceAbleObject.PlaceAbleObjectData.Undertile);
            }
            SetLayerRecursively(placeItem, objectLayer);
        }
    }
    public float RoundToNearestHalf(float value)
    {
        return Mathf.Round(value * 2) / 2;
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}

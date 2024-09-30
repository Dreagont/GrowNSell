using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    public Tilemap Ground;
    public GameObject[] BaseObjects;
    public Transform ObjectsParent;
    public List<Vector3> ObjectPosition = new List<Vector3>();
    private void Start()
    {
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
                        int objectIndex = Random.Range(0, BaseObjects.Length);
                        GameObject spawnedObject = Instantiate(BaseObjects[objectIndex], truePos, Quaternion.identity, ObjectsParent);
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
        int objectLayer = LayerMask.NameToLayer("Object");

        
        Vector3 truePos = Ground.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);
        truePos.x = RoundToNearestHalf(truePos.x);
        truePos.y = RoundToNearestHalf(truePos.y);
        if (Random.Range(1, 4) < 3)
        {
            int objectIndex = Random.Range(0, BaseObjects.Length);
            GameObject spawnedObject = Instantiate(BaseObjects[objectIndex], truePos, Quaternion.identity, ObjectsParent);
            ObjectPosition.Add(truePos);
            Object objectSpwaned = spawnedObject.GetComponent<Object>();
            objectSpwaned.ObjectPosition = truePos;

            SetLayerRecursively(spawnedObject, objectLayer);
        }
            
        
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
            placeAble.placeAbleObjectData = holdItem;

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

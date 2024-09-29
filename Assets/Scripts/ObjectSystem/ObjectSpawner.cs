using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    public Tilemap Ground;
    public GameObject[] BaseObjects;
    public Transform ObjectsParent;

    private void Start()
    {
        SpawnObject();
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

        if (Random.Range(1, 4) < 3)
        {
            int objectIndex = Random.Range(0, BaseObjects.Length);
            GameObject spawnedObject = Instantiate(BaseObjects[objectIndex], truePos, Quaternion.identity, ObjectsParent);
            Object objectSpwaned = spawnedObject.GetComponent<Object>();

            objectSpwaned.ObjectPosition = truePos;

            SetLayerRecursively(spawnedObject, objectLayer);
        }
            
        
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

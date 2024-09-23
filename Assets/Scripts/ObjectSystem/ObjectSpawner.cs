using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public Tilemap Ground;
    public GameObject[] objects;
    public Transform ObjectsParent;
    void Start()
    {
        //SpawnObject();
    }

    public void SpawnObject()
    {
        int objectLayer = LayerMask.NameToLayer("Object");

        foreach (var position in Ground.cellBounds.allPositionsWithin)
        {
            if (Ground.HasTile(position))
            {
                Vector3 truePos = Ground.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

                if (Random.Range(1, 4) < 3)
                {
                    int objectIndex = Random.Range(0, objects.Length);
                    GameObject spawnedObject = Instantiate(objects[objectIndex], truePos, Quaternion.identity, ObjectsParent);
                    Object objectSpwaned = spawnedObject.GetComponent<Object>();

                    objectSpwaned.ObjectPosition = truePos;

                    SetLayerRecursively(spawnedObject, objectLayer);
                }
            }
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

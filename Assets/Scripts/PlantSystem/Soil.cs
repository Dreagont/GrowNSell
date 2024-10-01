using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    public bool isWatered = false;
    public bool isSeeded = false;
    private ObjectsManager objectsManager;
    private TileManager tileManager;
    void Start()
    {
        isSeeded = false;
    }

    void Update()
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {

    }

    public void DeWater()
    {
        this.isWatered = false;
    }

    private void OnDestroy()
    {
        objectsManager = FindAnyObjectByType<ObjectsManager>();
        tileManager = FindAnyObjectByType<TileManager>();

        if (tileManager != null)
        {
            tileManager.setTileGrass(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z));
        }

        if (objectsManager != null)
        {
            objectsManager.Soils.Remove(this);
            objectsManager.SoilValues.Remove(gameObject.transform.position);
        }
    }

}

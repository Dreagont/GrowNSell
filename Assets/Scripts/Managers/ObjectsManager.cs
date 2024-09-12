using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public Dictionary<Vector3, bool> SoilValues = new Dictionary<Vector3, bool>();
    public List<Vector3> SeedValues = new List<Vector3>();
    public Soil[] Soils;
    void Start()
    {
    }

    void Update()
    {
        Soils = FindObjectsOfType<Soil>();

    }

    public void DeWaterAll()
    {
        List<Vector3> soilKeys = new List<Vector3>(SoilValues.Keys); 
        foreach (Vector3 soilPosition in soilKeys)
        {
            SoilValues[soilPosition] = false;
        }
        foreach (var soil in Soils)
        {
            soil.DeWater();
        }

    }
}

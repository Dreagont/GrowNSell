using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectsManager : MonoBehaviour
{
    public Dictionary<Vector3, bool> SoilValues = new Dictionary<Vector3, bool>();
    public Dictionary<Vector3, int> SeedValues = new Dictionary<Vector3, int>();
    public Tilemap WaterSoilMap;
    public Soil[] Soils;
    public Seed[] Seeds;

    void Update()
    {
        Soils = FindObjectsOfType<Soil>();
        Seeds = FindObjectsOfType<Seed>();
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
        foreach (var position in WaterSoilMap.cellBounds.allPositionsWithin)
        {
            if (WaterSoilMap.HasTile(position))
            {
                WaterSoilMap.SetTile(position, null);
            }
        }
    }

    public void WaterCheck()
    {
        foreach (var seed in Seeds)
        {
            if (!seed.thisSoil.isWatered)
            {
                seed.dayNotWater++;
            } else
            {
                seed.dayNotWater = 0;
            }
        }
    }

    public void UpdateSeedSpriteAndState()
    {
        foreach (var seed in Seeds)
        {
            if (seed.thisSoil.isWatered)  
            {
                seed.UpdateSeedSprite();  
                SeedValues[seed.position] = seed.currentState; 
                Debug.Log("Watered seed growing at position: " + seed.position);
            } else
            {
                seed.temp = 0;
            }
        }
    }
}

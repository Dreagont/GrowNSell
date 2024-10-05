using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandExpander : MonoBehaviour
{
    public Tilemap tilemap; 
    public RuleTile arenaTile;
    public Tilemap interactMap;
    public Tile interactTile;

    public int xConstrain = 0;
    public int xConstrain1 = 0;
    public int yConstrain = 0;
    public int yConstrain1 = 0;

    public Camera Camera;
    public ObjectSpawner ObjectSpawner;
    void Start()
    {
        GenerateArena(10, 10, 0);

    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GenerateArena(10, 10, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GenerateArena(10, 10, 2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GenerateArena(10, 10, 3);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            GenerateArena(10, 10, 4);
        }
    }*/

    public void GenerateArena(int width, int height, int side)
    {
        int xSize = 0;
        int ySize = 0;

        if (side == 1)
        {
            Camera.orthographicSize = 9;
            Camera.transform.position = new Vector3(5,0,-10);
            xSize = xConstrain / 2;
            ySize = (height) / 2;

            for (int x = xSize; x < xSize + width; x++)
            {
                for (int y = -ySize; y < ySize; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, arenaTile);
                    interactMap.SetTile(tilePosition, interactTile);
                    ObjectSpawner.SpawnObject(tilePosition);
                }
            }

            xConstrain += width;
        } else if (side == 2)
        {
            xSize = width / 2;
            ySize = yConstrain / 2;

            for (int x = -xSize; x < xSize; x++)
            {
                for (int y = ySize; y < ySize + height; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, arenaTile);
                    interactMap.SetTile(tilePosition, interactTile);
                    ObjectSpawner.SpawnObject(tilePosition);
                }
            }

            yConstrain += height;
        } else if (side == 3) 
        {
            xSize = width / 2;
            ySize = yConstrain1 / 2;  

            for (int x = -xSize; x < xSize; x++)
            {
                for (int y = yConstrain1 - height; y < yConstrain1; y++) 
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, arenaTile);
                    interactMap.SetTile(tilePosition, interactTile);
                    ObjectSpawner.SpawnObject(tilePosition);
                }
            }

            yConstrain1 -= height; 
        }
        else if (side == 4) 
        {
            xSize = xConstrain1 / 2;  
            ySize = height / 2;

            for (int x = xConstrain1 - width; x < xConstrain1; x++) 
            {
                for (int y = -ySize; y < ySize; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, arenaTile);
                    interactMap.SetTile(tilePosition, interactTile);
                    ObjectSpawner.SpawnObject(tilePosition);
                }
            }

            xConstrain1 -= width; 
        }
        else
        {
            xSize = (width) / 2;
            ySize = (height) / 2;


            for (int x = -xSize; x < xSize; x++)
            {
                for (int y = -ySize; y < ySize; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    interactMap.SetTile(tilePosition, interactTile);
                    tilemap.SetTile(tilePosition, arenaTile);
                    ObjectSpawner.SpawnObject(tilePosition);
                }
            }

            xConstrain += width;
            yConstrain += height;
            xConstrain1 -= width / 2;
            yConstrain1 -= height / 2;
        }
    }
}

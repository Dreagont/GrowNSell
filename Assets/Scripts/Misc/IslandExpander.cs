using UnityEngine;
using UnityEngine.Tilemaps;

public class IslandExpander : MonoBehaviour
{
    public Tilemap tilemap; // The Tilemap where RuleTiles are placed
    public RuleTile ruleTile; // The RuleTile used for the island
    public Vector3Int minBounds; // Minimum x, y coordinates of the island
    public Vector3Int maxBounds; // Maximum x, y coordinates of the island
    public int expansionSize = 10; // Size of the expansion area (10x10)

    public ObjectSpawner objectSpawner; // Reference to ObjectSpawner

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ExpandIsland("up");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ExpandIsland("down");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ExpandIsland("left");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ExpandIsland("right");
        }
    }

    public void ExpandIsland(string direction)
    {
        BoundsInt newBounds = new BoundsInt(); // This will store the newly expanded area bounds

        switch (direction.ToLower())
        {
            case "up":
                newBounds = ExpandUp();
                break;
            case "down":
                newBounds = ExpandDown();
                break;
            case "left":
                newBounds = ExpandLeft();
                break;
            case "right":
                newBounds = ExpandRight();
                break;
        }

        // Spawn objects only in the newly expanded area
        objectSpawner.SpawnObject(newBounds);
    }

    private BoundsInt ExpandUp()
    {
        int yMin = maxBounds.y + 1;
        int yMax = maxBounds.y + expansionSize;
        for (int x = minBounds.x; x <= maxBounds.x; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector3Int newPosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(newPosition, ruleTile);
            }
        }
        BoundsInt newBounds = new BoundsInt(minBounds.x, yMin, 0, maxBounds.x - minBounds.x + 1, expansionSize, 1);
        maxBounds.y += expansionSize;
        return newBounds;
    }

    private BoundsInt ExpandDown()
    {
        int yMin = minBounds.y - expansionSize;
        int yMax = minBounds.y - 1;
        for (int x = minBounds.x; x <= maxBounds.x; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                Vector3Int newPosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(newPosition, ruleTile);
            }
        }
        BoundsInt newBounds = new BoundsInt(minBounds.x, yMin, 0, maxBounds.x - minBounds.x + 1, expansionSize, 1);
        minBounds.y -= expansionSize;
        return newBounds;
    }

    private BoundsInt ExpandLeft()
    {
        int xMin = minBounds.x - expansionSize;
        int xMax = minBounds.x - 1;
        for (int y = minBounds.y; y <= maxBounds.y; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                Vector3Int newPosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(newPosition, ruleTile);
            }
        }
        BoundsInt newBounds = new BoundsInt(xMin, minBounds.y, 0, expansionSize, maxBounds.y - minBounds.y + 1, 1);
        minBounds.x -= expansionSize;
        return newBounds;
    }

    private BoundsInt ExpandRight()
    {
        int xMin = maxBounds.x + 1;
        int xMax = maxBounds.x + expansionSize;
        for (int y = minBounds.y; y <= maxBounds.y; y++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                Vector3Int newPosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(newPosition, ruleTile);
            }
        }
        BoundsInt newBounds = new BoundsInt(xMin, minBounds.y, 0, expansionSize, maxBounds.y - minBounds.y + 1, 1);
        maxBounds.x += expansionSize;
        return newBounds;
    }
}

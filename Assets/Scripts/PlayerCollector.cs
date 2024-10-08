using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollector : MonoBehaviour, IDataPersistence
{
    public Tilemap collectableTilemap; 
    public InventoryManager inventoryManager; 
    public Item[] collectableItems; 

    private HashSet<Vector3Int> initialCollectablePositions; // Store initial collectable positions

    private void Awake()
    {
        collectableTilemap = GameObject.Find("Collectables").GetComponent<Tilemap>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        initialCollectablePositions = new HashSet<Vector3Int>(); // Initialize the set

        // Save the positions of the initial collectable tiles
        foreach (Vector3Int position in collectableTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tileBase = collectableTilemap.GetTile(position);
            if (tileBase != null) // Only include positions with actual collectable tiles
            {
                initialCollectablePositions.Add(position);
            }
        }
    }

    private void Update()
    {
        Vector3 playerPosition = transform.position;
        CollectItemAtPosition(playerPosition);
    }

    void CollectItemAtPosition(Vector3 position)
    {
        Vector3Int gridPosition = collectableTilemap.WorldToCell(position);
        TileBase tileBase = collectableTilemap.GetTile(gridPosition);

        if (tileBase != null)
        {
            Debug.Log("Attempting to collect item at: " + gridPosition);
            
            Item collectedItem = GetItemFromTile(tileBase);
            if (collectedItem != null)
            {
                if (inventoryManager.AddItem(collectedItem))
                {
                    Debug.Log("Item collected. Removing tile at: " + gridPosition);
                    collectableTilemap.SetTile(gridPosition, null); // Remove the tile after collection
                }
            }
        }
    }

    Item GetItemFromTile(TileBase tileBase)
    {
        Tile tile = tileBase as Tile; 

        if (tile != null)
        {
            Sprite tileSprite = tile.sprite;

            foreach (var item in collectableItems)
            {
                if (item.image == tileSprite) 
                {
                    return item; 
                }
            }
        }
        return null; 
    }

    // SaveData implementation for IDataPersistence
    public void SaveData(ref GameData data)
    {
        // Clear the list of collectable tiles to avoid duplicates
        data.collectableTiles.Clear();

        // Iterate over the initial collectable positions and check if they've been collected
        foreach (Vector3Int position in initialCollectablePositions)
        {
            // Save the position if the tile is null (indicating it has been collected)
            if (collectableTilemap.GetTile(position) == null) // Only save collected tile positions
            {
                Debug.Log("Saving collected tile position: " + position);
                data.collectableTiles.Add(new TileData(position));
            }
        }
    }

    // LoadData implementation for IDataPersistence
    public void LoadData(GameData data)
    {
        // Loop through the collected tiles saved in the game data
        foreach (TileData tileData in data.collectableTiles)
        {
            Vector3Int gridPosition = tileData.tilePosition;

            // Remove tiles at saved collected positions
            if (collectableTilemap.HasTile(gridPosition))
            {
                Debug.Log("Removing tile at collected position: " + gridPosition);
                collectableTilemap.SetTile(gridPosition, null);
            }
        }
    }
}

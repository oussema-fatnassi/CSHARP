using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollector : MonoBehaviour, IDataPersistence
{
    public Tilemap collectableTilemap; 
    public InventoryManager inventoryManager; 
    public Item[] collectableItems; 

    private HashSet<Vector3Int> initialCollectablePositions;

    private void Awake()
    {
        collectableTilemap = GameObject.Find("Collectables").GetComponent<Tilemap>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        initialCollectablePositions = new HashSet<Vector3Int>();

        foreach (Vector3Int position in collectableTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tileBase = collectableTilemap.GetTile(position);
            if (tileBase != null) 
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
                    collectableTilemap.SetTile(gridPosition, null); 
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
    public void SaveData(ref GameData data)
    {
        data.collectableTiles.Clear();
        foreach (Vector3Int position in initialCollectablePositions)
        {
            if (collectableTilemap.GetTile(position) == null) 
            {
                Debug.Log("Saving collected tile position: " + position);
                data.collectableTiles.Add(new TileData(position));
            }
        }
    }

    public void LoadData(GameData data)
    {
        foreach (TileData tileData in data.collectableTiles)
        {
            Vector3Int gridPosition = tileData.tilePosition;
            if (collectableTilemap.HasTile(gridPosition))
            {
                Debug.Log("Removing tile at collected position: " + gridPosition);
                collectableTilemap.SetTile(gridPosition, null);
            }
        }
    }
}

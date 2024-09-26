using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollector : MonoBehaviour
{
    public Tilemap collectableTilemap; // Reference to the Tilemap with the collectable items
    public InventoryManager inventoryManager; // Reference to your inventory manager
    public Item[] collectableItems; // Predefined items that can be collected

    // Update is called once per frame
    private void Update()
    {
        // Check if the player is on a collectable tile each frame
        Vector3 playerPosition = transform.position;
        CollectItemAtPosition(playerPosition);
    }

    // Method to check if there is a collectable item at the player's position
    void CollectItemAtPosition(Vector3 position)
    {
        Vector3Int gridPosition = collectableTilemap.WorldToCell(position);
        TileBase tileBase = collectableTilemap.GetTile(gridPosition);

        if (tileBase != null)
        {
            Debug.Log("Item found at position: " + gridPosition);
            Item collectedItem = GetItemFromTile(tileBase);
            if (collectedItem != null)
            {
                if (inventoryManager.AddItem(collectedItem))
                {
                    // Successfully added item, remove from tilemap
                    collectableTilemap.SetTile(gridPosition, null); // Remove the item from the tilemap
                }
            }
        }
        else
        {
            Debug.Log("No item found at this position.");
        }
    }

    // Method to get the corresponding Item from a tile
// Method to get the corresponding Item from a tile
    Item GetItemFromTile(TileBase tileBase)
    {
        // Cast the TileBase to Tile to access the sprite
        Tile tile = tileBase as Tile; // Cast to Tile

        if (tile != null)
        {
            // Now we can access the sprite
            Sprite tileSprite = tile.sprite;

            // Compare the tile's sprite with your collectable items
            foreach (var item in collectableItems)
            {
                if (item.image == tileSprite) // Compare sprites
                {
                    return item; // Return the existing item reference
                }
            }
        }
        return null; // If no match found
    }
}

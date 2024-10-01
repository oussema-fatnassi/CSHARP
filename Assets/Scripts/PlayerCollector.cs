using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCollector : MonoBehaviour
{
    public Tilemap collectableTilemap; 
    public InventoryManager inventoryManager; 
    public Item[] collectableItems; 

    private void Update()
    {
        Vector3 playerPosition = transform.position;
        CollectItemAtPosition(playerPosition);
    }

    private void Awake()
    {
        collectableTilemap = GameObject.Find("Collectables").GetComponent<Tilemap>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    void CollectItemAtPosition(Vector3 position)
    {
        Vector3Int gridPosition = collectableTilemap.WorldToCell(position);
        TileBase tileBase = collectableTilemap.GetTile(gridPosition);

        if (tileBase != null)
        {
            Item collectedItem = GetItemFromTile(tileBase);
            if (collectedItem != null)
            {
                if (inventoryManager.AddItem(collectedItem))
                {
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
}

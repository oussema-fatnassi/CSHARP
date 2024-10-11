using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosition;
    public List<TileData> collectableTiles;
    public List<InventoryItemData> inventoryItems;
    public string playerName;
    public int totalCurrency;
    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);
        collectableTiles = new List<TileData>();
        inventoryItems = new List<InventoryItemData>();
        playerName = "";
        totalCurrency = 1000;
    }
}

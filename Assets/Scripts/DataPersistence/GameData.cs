using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
    This class is responsible for storing the game data that needs to be saved and loaded.
    It is used by the FileDataHandler class to serialize and deserialize the game data.
*/

[System.Serializable]
public class SerializablePlayerStats
{
    public string playerName;
    public PlayerStatsData stats;

    public SerializablePlayerStats(string name, PlayerStatsData data)
    {
        playerName = name;
        stats = data;
    }
}

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosition;
    public List<TileData> collectableTiles;
    public List<InventoryItemData> inventoryItems;
    public string playerName;
    public int totalCurrency;
    
    public List<SerializablePlayerStats> serializedPlayerStats;
    
    [System.NonSerialized]
    public Dictionary<string, PlayerStatsData> playerStats;
    // Constructor for the GameData class
    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);
        collectableTiles = new List<TileData>();
        inventoryItems = new List<InventoryItemData>();
        playerName = "";
        totalCurrency = 1000;
        playerStats = new Dictionary<string, PlayerStatsData>();
        serializedPlayerStats = new List<SerializablePlayerStats>();
    }
    // Serialize the game data before saving
    public void OnBeforeSerialize()
    {
        serializedPlayerStats = new List<SerializablePlayerStats>();
        if (playerStats != null)
        {
            foreach (var kvp in playerStats)
            {
                serializedPlayerStats.Add(new SerializablePlayerStats(kvp.Key, kvp.Value));
            }
        }
    }
    // Deserialize the game data after loading
    public void OnAfterDeserialize()
    {
        playerStats = new Dictionary<string, PlayerStatsData>();
        if (serializedPlayerStats != null)
        {
            foreach (var stats in serializedPlayerStats)
            {
                playerStats[stats.playerName] = stats.stats;
            }
        }
    }
}
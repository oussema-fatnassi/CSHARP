using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
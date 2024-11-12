using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/*
    This class is responsable for the debug of the data persistence of the game.
    Used to find the bugs in the data persistence of the game.
*/

public class DebugDataPersistence : MonoBehaviour
{
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        StartCoroutine(DebugPersistenceObjects());
    }
    // Debug the data persistence objects in the scene
    private System.Collections.IEnumerator DebugPersistenceObjects()
    {
        yield return null;

        var persistenceManager = DataPersistenceManager.instance;
        if (persistenceManager == null)
        {
            Debug.LogError("No DataPersistenceManager found in scene!");
            yield break;
        }

        var persistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
        Debug.Log($"Found {persistenceObjects.Count} objects implementing IDataPersistence:");
        foreach (var obj in persistenceObjects)
        {
            Debug.Log($"- {obj.GetType().Name} on GameObject: {((MonoBehaviour)obj).gameObject.name}");
        }
    }
    // Log the game data to the console
    public static void LogGameData(GameData gameData, string context)
    {
        if (gameData == null)
        {
            Debug.LogWarning($"[{context}] GameData is null!");
            return;
        }

        Debug.Log($"[{context}] GameData state:");
        Debug.Log($"- Position: {gameData.playerPosition}");
        Debug.Log($"- Currency: {gameData.totalCurrency}");
        Debug.Log($"- Collectables: {gameData.collectableTiles?.Count ?? 0}");
        Debug.Log($"- Inventory Items: {gameData.inventoryItems?.Count ?? 0}");
        Debug.Log($"- Player Stats Count: {gameData.serializedPlayerStats?.Count ?? 0}");

        if (gameData.serializedPlayerStats != null)
        {
            foreach (var playerStat in gameData.serializedPlayerStats)
            {
                Debug.Log($"  Player: {playerStat.playerName}");
                Debug.Log($"    Health: {playerStat.stats.health}/{playerStat.stats.maxHealth}");
                Debug.Log($"    Level: {playerStat.stats.level}");
                Debug.Log($"    Experience: {playerStat.stats.experience}");
            }
        }
    }
}
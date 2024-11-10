using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string fileName;
    [SerializeField] private PlayerStats[] allPlayerStats;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var playerStats in allPlayerStats)
        {
            playerStats.CacheInitialValues();
        }
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        
        if (allPlayerStats != null)
        {
            foreach (var playerStats in allPlayerStats)
            {
                if (playerStats != null && !dataPersistenceObjects.Contains(playerStats))
                {
                    dataPersistenceObjects.Add(playerStats);
                }
            }
        }
        else
        {
            Debug.LogError("No PlayerStats assigned to DataPersistenceManager!");
        }

        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        
        if (allPlayerStats != null)
        {
            foreach (var playerStats in allPlayerStats)
            {
                if (playerStats != null)
                {
                    playerStats.ResetToInitialValues();
                    playerStats.SaveData(ref gameData);
                    Debug.Log($"Initialized stats for player: {playerStats.playerName}");
                }
            }
        }

        SaveGame();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        
        if (this.gameData == null)
        {
            Debug.Log("No save data found. Starting new game.");
            NewGame();
        }
        else
        {
            Debug.Log("Loading game data...");
            foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData(gameData);
            }
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No game data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref gameData);
        }

        foreach (var entry in gameData.playerStats)
        {
            Debug.Log($"Saved stats for player: {entry.Key}");
            Debug.Log($"Health: {entry.Value.health}, MaxHealth: {entry.Value.maxHealth}");
            Debug.Log($"Level: {entry.Value.level}, Experience: {entry.Value.experience}");
        }

        dataHandler.Save(gameData);
        Debug.Log("Game data saved successfully!");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private string defaultFileName = "defaultSave";
    [SerializeField] private PlayerStats[] allPlayerStats;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    // Track the currently loaded save file
    private string currentSaveFileName;

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, defaultFileName);
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

        // Load the default save file when starting the game
        // LoadGame(defaultFileName);
    }

    // Create a new game with a specific save file name
    public void NewGame(string saveName)
    {
        this.gameData = new GameData();
        this.currentSaveFileName = saveName;

        foreach (var playerStats in allPlayerStats)
        {
            if (playerStats != null)
            {
                playerStats.ResetToInitialValues();
                playerStats.SaveData(ref gameData);
                Debug.Log($"Initialized stats for player: {playerStats.playerName}");
            }
        }

        SaveGame(currentSaveFileName);
    }

    // Save the game to a specific file name if provided, or use the current file
public void SaveGame(string saveName = null)
{
    if (this.gameData == null)
    {
        Debug.LogWarning("No game data found. Start a New Game first.");
        return;
    }

    foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
    {
        dataPersistenceObject.SaveData(ref gameData);
        Debug.Log($"Saving data for object: {dataPersistenceObject}");
    }

    string saveFileName = saveName ?? currentSaveFileName ?? defaultFileName;
    dataHandler.Save(gameData, saveFileName);
    Debug.Log($"Game data saved to {saveFileName} with data: {JsonUtility.ToJson(gameData)}");
}

public void LoadGame(string saveName = null)
{
    this.currentSaveFileName = saveName ?? defaultFileName;
    this.gameData = dataHandler.Load(currentSaveFileName);

    if (this.gameData == null)
    {
        Debug.Log("No save data found. Starting new game.");
        NewGame(currentSaveFileName);
    }
    else
    {
        Debug.Log($"Loading game data from {currentSaveFileName} with data: {JsonUtility.ToJson(gameData)}");
        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(gameData);
            Debug.Log($"Loaded data for object: {dataPersistenceObject}");
        }
    }
}


    public string[] GetAllSaveFiles()
    {
        return dataHandler.GetAllSaveFiles();
    }

    private void OnApplicationQuit()
    {
        SaveGame(); // Save using the current save file name
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
    }
}

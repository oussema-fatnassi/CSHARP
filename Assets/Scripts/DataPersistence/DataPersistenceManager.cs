using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Configuration")]
    [SerializeField] private PlayerStats[] allPlayerStats;
    private string currentSaveFile;
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(currentSaveFile))
        {
            SetSaveFile("defaultSave.json");
        }
    }

    public void SetSaveFile(string saveFileName)
    {
        Debug.Log($"Setting save file name to: {saveFileName}");
        this.currentSaveFile = saveFileName;

        string filePath = SaveSlotManager.GetSaveFilePath(saveFileName);
        Debug.Log($"Using file path: {filePath}");
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, filePath);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        Debug.Log("Creating new game data using default settings.");

        SaveGame(false);
        SceneManager.LoadScene("MainScene");
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load(currentSaveFile);

        if (this.gameData == null)
        {
            Debug.LogError("No save data found. Creating a new game data.");
            NewGame();
            return;
        }

        SceneManager.LoadScene("MainScene"); 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            InitializeGame();
            if (gameData != null)
            {
                foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
                {
                    dataPersistenceObject.LoadData(gameData);
                }
                Debug.Log("Game loaded successfully.");
            }
        }
    }

    public void SaveGame(bool updateGameData = true)
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No game data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        if (updateGameData)
        {
            UpdateGameData();
        }

        Debug.Log($"Saving game with save file name: {currentSaveFile}");
        
        dataHandler.Save(gameData, currentSaveFile);
        Debug.Log("Game saved successfully.");
    }

    public void UpdateGameData()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No game data available to update.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObject in dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref gameData);
        }

        Debug.Log("Game data updated.");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeGame()
    {
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

        Debug.Log("Game Initialized with all data persistence objects.");
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public string CurrentSaveFile
    {
        get { return currentSaveFile; }
    }

}

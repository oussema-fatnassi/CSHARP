using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/* 
    This class is responsible for managing the data persistence of the game.
    It is a singleton class that persists between scenes and is responsible for saving and loading the game data.
    It also initializes the game data and updates it when necessary.
    It uses the FileDataHandler class to save and load the game data to and from a file.
 */

public class DataPersistenceManager : MonoBehaviour
{
    // Singleton instance of the DataPersistenceManager class
    [Header("File Storage Configuration")]
    [SerializeField] private PlayerStats[] allPlayerStats;                                                                     
    private string currentSaveFile;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    // Awake is called when the script instance is being loaded
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
    // Start is called before the first frame update and is used to initialize the game data
    private void Start()
    {
        if (string.IsNullOrEmpty(currentSaveFile))
        {
            SetSaveFile("defaultSave.json");
        }
    }
    // Set the save file name to the specified value
    public void SetSaveFile(string saveFileName)
    {
        Debug.Log($"Setting save file name to: {saveFileName}");
        this.currentSaveFile = saveFileName;

        string filePath = SaveSlotManager.GetSaveFilePath(saveFileName);
        Debug.Log($"Using file path: {filePath}");
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, filePath);
    }
    // Create a new game with default settings
    public void NewGame()
    {
        this.gameData = new GameData();
        Debug.Log("Creating new game data using default settings.");

        SaveGame(false);
        SceneManager.LoadScene("MainScene");
    }
    // Load the game data from the current save file and load the main scene
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
    // Save the game data to the current save file and update the game data
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
    // Save the game data to the current save file
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
    // Update the game data using the data persistence objects
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
    // Save the game data when the application is quit
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    // Unsubscribe from the scene loaded event when the script is disabled
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Initialize the game data by finding all data persistence objects in the scene
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
    // Find all data persistence objects in the scene
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    // Get the current save file name
    public string CurrentSaveFile
    {
        get { return currentSaveFile; }
    }

}

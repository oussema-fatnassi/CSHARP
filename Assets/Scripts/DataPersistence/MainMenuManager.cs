using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

/*
    This class is responsible for managing the main menu of the game.
    It handles the creation of new games, loading of saved games, and quitting the game.
*/

public class MainMenuManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] public GameObject mainMenuPanel;
    [SerializeField] public GameObject newGamePanel;
    [SerializeField] public GameObject loadGamePanel;
    
    [Header("Main Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button quitButton;
    
    [Header("New Game")]
    [SerializeField] private TMP_InputField saveNameInput;
    [SerializeField] private Button createGameButton;
    [SerializeField] private TextMeshProUGUI newGameErrorText;
    [SerializeField] private Button newGameBackButton;

    [Header("Load Game")]
    [SerializeField] public Transform saveFileContainer;
    [SerializeField] public GameObject saveFileButtonPrefab;
    [SerializeField] public TextMeshProUGUI noSavesText;
    [SerializeField] private Button loadGameBackButton;
    
    [Header("Scene Management")]
    [SerializeField] private string gameSceneName = "GameScene";

    private void Awake()
    {
        Debug.Log("MainMenuManager Awake");
        Debug.Log($"Main Menu Panel null? {mainMenuPanel == null}");
        Debug.Log($"New Game Panel null? {newGamePanel == null}");
        Debug.Log($"Load Game Panel null? {loadGamePanel == null}");
    }
    // Show the main menu panel and hide the other panels when the game starts
    private void Start()
    {
        Debug.Log("MainMenuManager Start");
        
        if (newGameButton != null)
            newGameButton.onClick.AddListener(() => {
                Debug.Log("New Game button clicked");
                ShowNewGame();
            });
            
        if (loadGameButton != null)
            loadGameButton.onClick.AddListener(() => {
                Debug.Log("Load Game button clicked");
                ShowLoadGame();
            });
            
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
            
        if (createGameButton != null)
            createGameButton.onClick.AddListener(CreateNewGame);
            
        if (newGameBackButton != null)
            newGameBackButton.onClick.AddListener(ShowMainMenu);
            
        if (loadGameBackButton != null)
            loadGameBackButton.onClick.AddListener(ShowMainMenu);

        ShowMainMenu();
    }
    // Show the main menu panel
    public void ShowMainMenu()
    {
        Debug.Log("Showing Main Menu");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (newGamePanel != null) newGamePanel.SetActive(false);
        if (loadGamePanel != null) loadGamePanel.SetActive(false);
    }
    // Show the new game panel
    public void ShowNewGame()
    {
        Debug.Log("Showing New Game Panel");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (newGamePanel != null) newGamePanel.SetActive(true);
        if (loadGamePanel != null) loadGamePanel.SetActive(false);
        
        if (saveNameInput != null)
        {
            saveNameInput.text = "";
        }
        if (newGameErrorText != null)
        {
            newGameErrorText.text = "";
        }
    }
    // Show the load game panel
    public void ShowLoadGame()
    {
        Debug.Log("Showing Load Game Panel");
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (newGamePanel != null) newGamePanel.SetActive(false);
        if (loadGamePanel != null) loadGamePanel.SetActive(true);
        
        RefreshSaveFileList();
    }
    // Refresh the list of save files in the load game panel by creating buttons for each save file
    private void RefreshSaveFileList()
    {
        Debug.Log("Refreshing save file list");
        if (saveFileContainer == null)
        {
            Debug.LogError("Save file container is null!");
            return;
        }

        foreach (Transform child in saveFileContainer)
        {
            Destroy(child.gameObject);
        }

        List<string> saveFiles = SaveSlotManager.GetAllSaveFiles();
        Debug.Log($"Found {saveFiles.Count} save files");

        if (saveFiles.Count == 0)
        {
            if (noSavesText != null)
            {
                noSavesText.gameObject.SetActive(true);
                Debug.Log("No saves found, showing message");
            }
            return;
        }
        
        if (noSavesText != null)
        {
            noSavesText.gameObject.SetActive(false);
        }

        foreach (string saveName in saveFiles)
        {
            GameObject buttonObj = Instantiate(saveFileButtonPrefab, saveFileContainer);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = saveName;
            }

            Button deleteButton = buttonObj.transform.Find("DeleteButton")?.GetComponent<Button>();
            if (deleteButton != null)
            {
                deleteButton.onClick.AddListener(() => DeleteSaveFile(saveName));
            }

            button.onClick.AddListener(() => LoadGame(saveName));
            
            Debug.Log($"Created button for save: {saveName}");
        }
    }

    private void DeleteSaveFile(string saveName)
    {
        Debug.Log($"Deleting save file: {saveName}");
        SaveSlotManager.DeleteSave(saveName);
        RefreshSaveFileList();
    }
    // Create a new game with the specified save name
    private void CreateNewGame()
    {
        Debug.Log("Creating new game...");
        string saveName = saveNameInput.text.Trim();

        if (string.IsNullOrEmpty(saveName))
        {
            newGameErrorText.text = "Please enter a save name";
            Debug.Log("Save name is empty");
            return;
        }

        if (SaveSlotManager.DoesSaveExist(saveName))
        {
            if (saveName != DataPersistenceManager.instance.CurrentSaveFile)
            {
                newGameErrorText.text = "A save with this name already exists";
                Debug.Log($"Save {saveName} already exists");
                return;
            }
        }

        Debug.Log($"Initializing new save file: {saveName}");
        DataPersistenceManager.instance.SetSaveFile(saveName);
        DataPersistenceManager.instance.NewGame();
        
        Debug.Log($"Loading game scene: {gameSceneName}");
        SceneManager.LoadScene(gameSceneName);
    }
    // Load the game with the specified save name
    private void LoadGame(string saveName)
    {
        Debug.Log($"Loading game: {saveName}");
        DataPersistenceManager.instance.SetSaveFile(saveName);
        DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadScene(gameSceneName);
    }
    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
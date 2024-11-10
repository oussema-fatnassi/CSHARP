using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject newGamePanel;
    [SerializeField] private GameObject loadGamePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject saveFileContainer;
    [SerializeField] private GameObject saveFileButtonPrefab;
    [SerializeField] private TMP_InputField saveNameInputField;
    [SerializeField] private TextMeshProUGUI errorText;

    private void Start()
    {
        mainMenuPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        errorText.text = "";
    }

    public void OpenNewGamePanel()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    public void CreateNewGame()
  {
      string saveName = saveNameInputField.text;
      if (string.IsNullOrEmpty(saveName))
      {
          errorText.text = "Please enter a save name.";
          return;
      }

      // Debugging logs to check each reference
      Debug.Log("Save Name: " + saveName);
      Debug.Log("DataPersistenceManager.instance: " + DataPersistenceManager.instance);
      
      if (DataPersistenceManager.instance == null)
      {
          Debug.LogError("DataPersistenceManager instance is not set. Ensure it is in the scene.");
          return;
      }

      DataPersistenceManager.instance.NewGame(saveName);
      SceneManager.LoadScene("MainScene");
  }


    public void OpenLoadGamePanel()
    {
        mainMenuPanel.SetActive(false);
        loadGamePanel.SetActive(true);

        // Clear previous save buttons
        foreach (Transform child in saveFileContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate save files
        string[] saveFiles = DataPersistenceManager.instance.GetAllSaveFiles();
        foreach (string saveFile in saveFiles)
        {
            GameObject button = Instantiate(saveFileButtonPrefab, saveFileContainer.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = saveFile;
            button.GetComponent<Button>().onClick.AddListener(() => LoadGame(saveFile));
        }
    }

    public void LoadGame(string saveName)
    {
        DataPersistenceManager.instance.LoadGame(saveName);
        SceneManager.LoadScene("MainScene");
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        saveNameInputField.text = "";
        errorText.text = "";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

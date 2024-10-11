using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class MenuController : MonoBehaviour
{
    // References to UI Panels
    public GameObject playPanel;         // Panel with Continue/Start New Game options
    public GameObject creditsPanel;      // Panel with developer information
    public GameObject mainMenuPanel;     // Main Menu Panel

    // This function is called when the Play button is clicked
    public void OnPlayButtonClick()
    {
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    // This function is called when the Credits button is clicked
    public void OnCreditsButtonClick()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // This function is called when the Back button is clicked in the Credits panel
    public void OnBackButtonClick()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // This function is called when Continue is clicked in the Play panel
    public void OnContinueButtonClick()
    {
        // Check if there's saved progress
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // Load the saved scene
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
            Debug.Log("No saved game found!");
            // Optionally, show a message to the user that no save exists
        }
    }

    // This function is called when Start New Game is clicked in the Play panel
    public void OnStartNewGameButtonClick()
    {
        // Clear any saved progress before starting a new game
        PlayerPrefs.DeleteKey("SavedScene");

        // Load the first scene (use the name or build index)
        SceneManager.LoadScene("1");  // Replace "Level1" with your actual first scene name or build index
    }

    // Save the current scene (this should be called from your game when the player quits or reaches a checkpoint)
    public void SaveGame(string currentScene)
    {
        PlayerPrefs.SetString("SavedScene", currentScene);
        PlayerPrefs.Save();
    }
}

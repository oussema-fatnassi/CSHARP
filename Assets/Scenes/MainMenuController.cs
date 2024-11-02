using UnityEngine;
using UnityEngine.SceneManagement;  // Needed for scene management
using UnityEngine.UI;               // Needed for UI components

public class MainMenuController : MonoBehaviour
{
    // Panels for each section
    public GameObject playPanel;
    public GameObject continuePanel;
    public GameObject creditsPanel;
    
    // Main menu buttons
    public GameObject mainMenu;

    void Start()
    {
        // Initially make sure all the panels are hidden
        playPanel.SetActive(true);
        continuePanel.SetActive(true);
        creditsPanel.SetActive(true);
        mainMenu.SetActive(true);
    }

    // Play Button Clicked
    public void OnPlayButton()
    {
        mainMenu.SetActive(true);
        playPanel.SetActive(false); // Show the Play Panel (Start New Game Confirmation)
    }

    // Continue Button Clicked
    public void OnContinueButton()
    {
        mainMenu.SetActive(false);
        continuePanel.SetActive(true); // Show Continue Panel (Confirm Load Save)
    }

    // Credits Button Clicked
    public void OnCreditsButton()
    {
        mainMenu.SetActive(false);
        creditsPanel.SetActive(true); // Show the Credits Panel
    }

    // Quit Button Clicked
    public void OnQuitButton()
    {
        Application.Quit();  // Quit the game
    }

    // Play Panel Yes Button (Starts New Game)
    public void OnPlayYesButton()
    {
        // Logic to start a new game (e.g., loading a scene)
        SceneManager.LoadScene("Stats"); // Load the new game scene
    }

    // Play Panel No Button (Return to Menu)
    public void OnPlayNoButton()
    {
        playPanel.SetActive(false);
        mainMenu.SetActive(true); // Return to main menu
    }

    // Continue Panel Yes Button (Loads Saved Game)
    public void OnContinueYesButton()
    {
        // Logic to load the saved game (e.g., loading a save file)
        LoadSavedGame(); // Custom function to load a save file
    }

    // Continue Panel No Button (Return to Menu)
    public void OnContinueNoButton()
    {
        continuePanel.SetActive(false);
        mainMenu.SetActive(true); // Return to main menu
    }

    // Credits Panel Back Button (Return to Menu)
    public void OnCreditsBackButton()
    {
        creditsPanel.SetActive(false);
        mainMenu.SetActive(true); // Return to main menu
    }

    // Custom function for loading saved game (can be expanded with actual save logic)
    private void LoadSavedGame()
    {
        // This would load your saved game; for now, we simulate by loading a scene
        SceneManager.LoadScene("Stats"); // Load saved game scene
    }
}

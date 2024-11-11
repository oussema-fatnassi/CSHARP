using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MainMenuManagerPlayModeTest
{
    private GameObject mainMenuManagerObject;
    private MainMenuManager mainMenuManager;

    // Mock UI panels and save container
    private GameObject mainMenuPanel;
    private GameObject newGamePanel;
    private GameObject loadGamePanel;
    private GameObject saveFileContainer;

    [SetUp]
    public void Setup()
    {
        mainMenuManagerObject = new GameObject("MainMenuManager");
        mainMenuManager = mainMenuManagerObject.AddComponent<MainMenuManager>();

        mainMenuPanel = new GameObject("MainMenuPanel");
        newGamePanel = new GameObject("NewGamePanel");
        loadGamePanel = new GameObject("LoadGamePanel");

        mainMenuPanel.transform.SetParent(mainMenuManagerObject.transform);
        newGamePanel.transform.SetParent(mainMenuManagerObject.transform);
        loadGamePanel.transform.SetParent(mainMenuManagerObject.transform);

        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);

        mainMenuManager.mainMenuPanel = mainMenuPanel;
        mainMenuManager.newGamePanel = newGamePanel;
        mainMenuManager.loadGamePanel = loadGamePanel;

        saveFileContainer = new GameObject("SaveFileContainer");
        saveFileContainer.transform.SetParent(mainMenuManagerObject.transform);
        mainMenuManager.saveFileContainer = saveFileContainer.transform;

        mainMenuManager.saveFileButtonPrefab = new GameObject("SaveFileButtonPrefab");
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(mainMenuManagerObject);
        Object.DestroyImmediate(mainMenuPanel);
        Object.DestroyImmediate(newGamePanel);
        Object.DestroyImmediate(loadGamePanel);
        Object.DestroyImmediate(saveFileContainer);
    }

    [UnityTest]
    public IEnumerator ShowMainMenu_ActivatesMainMenuPanelOnly()
    {
        mainMenuManager.ShowMainMenu();

        yield return null;

        Assert.IsTrue(mainMenuPanel.activeSelf, "Main menu panel should be active.");
        Assert.IsFalse(newGamePanel.activeSelf, "New game panel should be inactive.");
        Assert.IsFalse(loadGamePanel.activeSelf, "Load game panel should be inactive.");
    }

    [UnityTest]
    public IEnumerator ShowNewGame_ActivatesNewGamePanelOnly()
    {
        mainMenuManager.ShowNewGame();

        yield return null;

        Assert.IsTrue(newGamePanel.activeSelf, "New game panel should be active.");
        Assert.IsFalse(mainMenuPanel.activeSelf, "Main menu panel should be inactive.");
        Assert.IsFalse(loadGamePanel.activeSelf, "Load game panel should be inactive.");
    }
}

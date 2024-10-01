using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;


public class PlayerSelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerIconPrefabs; 
    [SerializeField] private List<GameObject> playerPrefabs;     
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private TMP_Text playerExperienceText;
    [SerializeField] private PlayerSlot[] playerSlots;
    [SerializeField] private CinemachineVirtualCameraBase playerCamera;
    private Player selectedPlayer;  
    private PlayerSlot currentSelectedSlot;
    public static PlayerSelectionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (i < playerIconPrefabs.Count)
            {
                GameObject playerIcon = Instantiate(playerIconPrefabs[i], playerSlots[i].transform);
                RectTransform rectTransform = playerIcon.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.anchorMin = new Vector2(.2f, .2f);
                rectTransform.anchorMax = new Vector2(.8f, .8f);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

                // Get the name of the player slot
                string slotName = playerSlots[i].SlotName; // Use the property to get the slot name

                Player playerData = null;
                // Check the slot name to determine which player type to instantiate
                if (slotName.Contains("Dragon"))
                {
                    playerData = playerIcon.AddComponent<Dragon>();
                }
                else if (slotName.Contains("Dwarf"))
                {
                    playerData = playerIcon.AddComponent<Dwarf>(); // Assuming you have a Dwarf class
                }
                else if (slotName.Contains("Elf"))
                {
                    playerData = playerIcon.AddComponent<Elf>(); // Assuming you have an Elf class
                }
                else if (slotName.Contains("Knight"))
                {
                    playerData = playerIcon.AddComponent<Knight>(); // Assuming you have a Knight class
                }
                else if (slotName.Contains("Mage"))
                {
                    playerData = playerIcon.AddComponent<Mage>(); // Assuming you have a Mage class
                }

                // Ensure playerData is not null before assigning to the player slot
                if (playerData != null)
                {
                    playerSlots[i].SetPlayerPrefab(playerIcon, playerData);
                }
                else
                {
                    Debug.LogError($"No valid player class found for slot: {slotName}");
                }
            }
        }
    }

    public void ShowPlayerStats(Player player)
    {
        if (player == null)
        {
            Debug.LogError("No player selected!");
            return;
        }

        Debug.Log($"Showing stats for: {player.PlayerName}, Level: {player.Level}, Experience: {player.Experience}");
        playerNameText.text = player.PlayerName;
        playerLevelText.text = "LVL: " + player.Level.ToString();
        playerExperienceText.text = "EXP: " + player.Experience.ToString();

        selectedPlayer = player; 
    }
    public void SetSelectedSlot(PlayerSlot slot)
    {
        // Reset the color of the previously selected slot
        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.ResetColor();
        }

        // Set the new selected slot
        currentSelectedSlot = slot;
    }

    public void SpawnPlayer()
    {
        if (selectedPlayer == null)
        {
            Debug.LogError("No player selected!");
            return;
        }

        GameObject spawnPoint = GameObject.FindWithTag("Player");

        if (spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.transform.position;
            Quaternion spawnRotation = spawnPoint.transform.rotation;

            if (spawnPoint.transform.childCount > 0)
            {
                Transform currentPlayer = spawnPoint.transform.GetChild(0);

                spawnPosition = currentPlayer.position;
                spawnRotation = currentPlayer.rotation;

                Destroy(currentPlayer.gameObject);
            }

            GameObject newPlayer = Instantiate(selectedPlayer.gameObject, spawnPosition, spawnRotation);

            newPlayer.transform.SetParent(spawnPoint.transform);
            playerCamera.Follow = newPlayer.transform;
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
        }
    }

}

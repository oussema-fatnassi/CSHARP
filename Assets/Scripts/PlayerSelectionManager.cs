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
    [SerializeField] private List<PlayerStats> playerStatsList; 
    [SerializeField] private CinemachineVirtualCameraBase playerCamera;
    private Player selectedPlayer;  
    private PlayerSlot currentSelectedSlot;
    private GameObject selectedPlayerPrefab;
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
                string slotName = playerSlots[i].SlotName;

                Player playerData = null;
                PlayerStats playerStats = FindPlayerStats(slotName); // Get the corresponding PlayerStats

                if (slotName.Contains("Dragon"))
                {
                    playerData = playerIcon.AddComponent<Dragon>();
                }
                else if (slotName.Contains("Dwarf"))
                {
                    playerData = playerIcon.AddComponent<Dwarf>();
                }
                else if (slotName.Contains("Elf"))
                {
                    playerData = playerIcon.AddComponent<Elf>();
                }
                else if (slotName.Contains("Knight"))
                {
                    playerData = playerIcon.AddComponent<Knight>();
                }
                else if (slotName.Contains("Mage"))
                {
                    playerData = playerIcon.AddComponent<Mage>();
                }

                if (playerData != null && playerStats != null)
                {
                    playerData.InitializePlayer(playerStats);
                    playerSlots[i].SetPlayerPrefab(playerIcon, playerData, i); 
                }
                else
                {
                    Debug.LogError($"No valid player class or stats found for slot: {slotName}");
                }
            }
        }
    }

    private PlayerStats FindPlayerStats(string playerName)
    {
        foreach (PlayerStats stats in playerStatsList)
        {
            if (stats.playerName == playerName)
            {
                return stats; 
            }
        }
        Debug.LogError($"No PlayerStats found for {playerName}");
        return null;
    }

    public void ShowPlayerStats(Player player, int playerIndex)
    {
        if (player == null || playerIndex < 0 || playerIndex >= playerPrefabs.Count)
        {
            Debug.LogError("No valid player or playerIndex out of bounds!");
            return;
        }

        Debug.Log($"Showing stats for: {player.PlayerName}, Level: {player.Level}, Experience: {player.Experience}");
        playerNameText.text = player.PlayerName;
        playerLevelText.text = "LVL: " + player.Level.ToString();
        playerExperienceText.text = "EXP: " + player.Experience.ToString();

        selectedPlayerPrefab = playerPrefabs[playerIndex]; 
        selectedPlayer = player; 

        Debug.Log("Selected player is now: " + selectedPlayer.PlayerName);
    }

    public void SpawnPlayer()
    {
        if (selectedPlayer == null)
        {
            Debug.LogError("No player selected!");
            return;
        }

        Debug.Log($"Spawning player: {selectedPlayer.PlayerName}");

        GameObject spawnPoint = GameObject.FindWithTag("PlayerContainer");

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

            GameObject newPlayer = Instantiate(selectedPlayerPrefab.gameObject, spawnPosition, spawnRotation); 
            newPlayer.transform.SetParent(spawnPoint.transform);
            playerCamera.Follow = newPlayer.transform;

            Debug.Log("Player spawned successfully!");
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
        }
    }
}

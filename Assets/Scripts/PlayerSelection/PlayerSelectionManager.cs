using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

/*
    This class is responsible for managing the player selection in the game.
    It handles the selection of the player, showing the player stats, and spawning the player in the game world.
*/

public class PlayerSelectionManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private List<GameObject> playerIconPrefabs; 
    [SerializeField] public List<GameObject> playerPrefabs;     
    [SerializeField] private List<TMP_Text> playerStatsText;
    [SerializeField] private PlayerSlot[] playerSlots;
    [SerializeField] public List<PlayerStats> playerStatsList; 
    [SerializeField] private CinemachineVirtualCameraBase playerCamera;
    private Player selectedPlayer;  
    private GameObject selectedPlayerPrefab;
    public static PlayerSelectionManager Instance { get; private set; }
    private Dictionary<string, System.Type> playerClassMap;
    private Dictionary<TMP_Text, System.Func<PlayerStats, string>> statsTextMap;

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
        InitializePlayerClassMap();
        SetupPlayerSlots();
    }
    // Initialize the player class map with the available player classes
    public void InitializePlayerClassMap()
    {
        playerClassMap = new Dictionary<string, System.Type>
        {
            { "Dragon", typeof(Dragon) },
            { "Dwarf", typeof(Dwarf) },
            { "Elf", typeof(Elf) },
            { "Knight", typeof(Knight) },
            { "Mage", typeof(Mage) }
        };
    }
    // Initialize the player stats text map with the available player stats
    private void InitializeStatsTextMap(PlayerStats stats)
    {
        statsTextMap = new Dictionary<TMP_Text, System.Func<PlayerStats, string>>();

        Dictionary<string, System.Func<PlayerStats, string>> statMappings = new Dictionary<string, System.Func<PlayerStats, string>>
        {
            { "HealthText", s => "HP: " + s.health.ToString() },
            { "ManaText", s => "Mana: " + s.mana.ToString() },
            { "LevelText", s => "LVL: " + s.level.ToString() },
            { "ExperienceText", s => "EXP: " + s.experience.ToString() },
            { "DamageText", s => "Damage: " + s.damage.ToString() },
            { "DefenseText", s => "Defense: " + s.defense.ToString() },
            { "SpeedText", s => "Speed: " + s.speed.ToString() },
            { "IntelligenceText", s => "Intelligence: " + s.intelligence.ToString() },
            { "PrecisionText", s => "Precision: " + s.precision.ToString() },
            { "PlayerNameText", s => s.playerName }
        };

        foreach (var mapping in statMappings)
        {
            TMP_Text textObj = playerStatsText.Find(t => t.name == mapping.Key);
            if (textObj != null)
            {
                statsTextMap.Add(textObj, mapping.Value);
            }
        }
    }
    // Setup the player slots with the player icons and stats
    private void SetupPlayerSlots()
    {
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (i >= playerIconPrefabs.Count) continue;

            GameObject playerIcon = CreatePlayerIcon(i);
            PlayerStats playerStats = FindPlayerStats(playerSlots[i].SlotName);

            if (playerStats == null) continue;

            Player playerData = AddPlayerComponent(playerIcon, playerSlots[i].SlotName);
            if (playerData != null)
            {
                playerData.InitializePlayer(playerStats);
                playerSlots[i].SetPlayerPrefab(playerIcon, playerData, i); 
            }
            else
            {
                LogError($"No valid player class found for slot: {playerSlots[i].SlotName}");
            }
        }
    }
    // Create the player icon for the specified index
    private GameObject CreatePlayerIcon(int index)
    {
        GameObject playerIcon = Instantiate(playerIconPrefabs[index], playerSlots[index].transform);
        RectTransform rectTransform = playerIcon.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = new Vector2(.2f, .2f);
        rectTransform.anchorMax = new Vector2(.8f, .8f);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        return playerIcon;
    }
    // Add the player component to the player icon
    private Player AddPlayerComponent(GameObject playerIcon, string slotName)
    {
        foreach (var entry in playerClassMap)
        {
            if (slotName.Contains(entry.Key))
            {
                return playerIcon.AddComponent(entry.Value) as Player;
            }
        }
        return null;
    }
    // Find the player stats for the specified player name
    private PlayerStats FindPlayerStats(string playerName)
    {
        foreach (PlayerStats stats in playerStatsList)
        {
            if (stats.playerName == playerName)
            {
                return stats; 
            }
        }
        LogError($"No PlayerStats found for {playerName}");
        return null;
    }
    // Show the player stats for the specified player and player index
    public void ShowPlayerStats(Player player, int playerIndex)
    {
        if (player == null || playerIndex < 0 || playerIndex >= playerPrefabs.Count)
        {
            LogError("No valid player or playerIndex out of bounds!");
            return;
        }

        selectedPlayerPrefab = playerPrefabs[playerIndex]; 
        selectedPlayer = player; 

        PlayerStats stats = FindPlayerStats(player.PlayerName);
        if (stats == null)
        {
            LogError("Player stats not found!");
            return;
        }

        if (statsTextMap == null)
        {
            InitializeStatsTextMap(stats);
        }

        foreach (var entry in statsTextMap)
        {
            if (entry.Key != null) 
            {
                entry.Key.text = entry.Value(stats);
            }
        }

        Debug.Log($"Selected player is now: {selectedPlayer.PlayerName}");
    }
    // Spawn the player in the game world
    public void SpawnPlayer()
    {
        if (selectedPlayer == null)
        {
            LogError("No player selected!");
            return;
        }

        Log($"Spawning player: {selectedPlayer.PlayerName}");
        GameObject spawnPoint = GameObject.FindWithTag("PlayerContainer");

        if (spawnPoint != null)
        {
            SpawnAtPoint(spawnPoint);
        }
        else
        {
            LogError("No GameObject with tag 'Player' found!");
        }
    }
    // Spawn the player at the specified spawn point
    private void SpawnAtPoint(GameObject spawnPoint)
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

        Log("Player spawned successfully!");
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }

    private void LogError(string message)
    {
        Debug.LogError(message);
    }
    // Save the player data to the game data
    public void SaveData(ref GameData data)
    {
        string name = GameObject.FindWithTag("PlayerContainer").transform.GetChild(0).name;
        name = name.Replace("(Clone)", "").Trim();
        data.playerName = name;
        data.playerPosition = GameObject.FindWithTag("PlayerContainer").transform.GetChild(0).position;
    }
    // Load the player data from the game data
    public void LoadData(GameData data)
    {
        GameObject prefabToInstantiate = playerPrefabs.Find(prefab => prefab.name == data.playerName);
        if (prefabToInstantiate != null)
        {
            GameObject spawnPoint = GameObject.FindWithTag("PlayerContainer");

            if (spawnPoint != null)
            {
                if (spawnPoint.transform.childCount > 0)
                {
                    foreach (Transform child in spawnPoint.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
                GameObject playerInstance = Instantiate(prefabToInstantiate, data.playerPosition, Quaternion.identity); 
                playerInstance.transform.SetParent(spawnPoint.transform); 
                playerCamera.Follow = playerInstance.transform;
                Debug.Log($"Loaded player: {data.playerName} at position: {data.playerPosition}");
            }
            else
            {
                Debug.LogError("No GameObject with tag 'PlayerContainer' found!");
            }
        }
        else
        {
            data.playerName = "Dragon";
        }
    }
}

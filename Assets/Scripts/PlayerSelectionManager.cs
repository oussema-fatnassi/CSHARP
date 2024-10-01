using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerIconPrefabs; 
    [SerializeField] private List<GameObject> playerPrefabs;     
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private TMP_Text playerExperienceText;
    [SerializeField] private PlayerSlot[] playerSlots;

    private Player selectedPlayer;  

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

                Player playerData = playerPrefabs[i].GetComponent<Player>();

                playerSlots[i].SetPlayerPrefab(playerIcon, playerData);
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
        }
        else
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectionManager : MonoBehaviour
{
    [SerializeField] private List<Player> players;
    [SerializeField] private List<GameObject> playerPrefabs;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private TMP_Text playerExperienceText;
    [SerializeField] private PlayerSlot[] playerSlots;

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
            if (i < playerPrefabs.Count)
            {
                GameObject playerIcon = Instantiate(playerPrefabs[i], playerSlots[i].transform);
                RectTransform rectTransform = playerIcon.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.anchorMin = new Vector2(.2f, .2f);
                rectTransform.anchorMax = new Vector2(.8f, .8f);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;

                // Get the name of the player slot
                string slotName = playerSlots[i].name;

                Player player = null;
                // Check the slot name to determine which player type to instantiate
                if (slotName.Contains("Dragon"))
                {
                    player = playerIcon.AddComponent<Dragon>();
                }
                else if (slotName.Contains("Dwarf"))
                {
                    player = playerIcon.AddComponent<Dwarf>(); // Assuming you have a Dwarf class
                }
                else if (slotName.Contains("Elf"))
                {
                    player = playerIcon.AddComponent<Elf>(); // Assuming you have an Elf class
                }
                else if (slotName.Contains("Knight"))
                {
                    player = playerIcon.AddComponent<Knight>(); // Assuming you have a Knight class
                }
                else if (slotName.Contains("Mage"))
                {
                    player = playerIcon.AddComponent<Mage>(); // Assuming you have an Elf class
                }

                players.Add(player);
                playerSlots[i].SetPlayerPrefab(playerIcon, player);
            }
        }
    }



    public void ShowPlayerStats(Player player)
    {
        Debug.Log($"Showing stats for: {player.PlayerName}, Level: {player.Level}, Experience: {player.Experience}");
        playerNameText.text = player.PlayerName;
        playerLevelText.text = "LVL: " + player.Level.ToString();
        playerExperienceText.text = "EXP: " + player.Experience.ToString();
    }

}

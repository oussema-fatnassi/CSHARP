using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform iconArea; 
    private GameObject instantiatedPrefab;
    private Player player;  
    public Image image;
    private int playerIndex;  // Store the index of the player

    public string SlotName => gameObject.name;

    // Method to set playerPrefab and player data, including the player index
    public void SetPlayerPrefab(GameObject playerPrefab, Player playerData, int index)
    {
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
        }

        instantiatedPrefab = playerPrefab;
        player = playerData;  
        playerIndex = index;  // Store the player index for later use
    }

    // Called when this slot is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)  // Check for double-click
        {
            if (player != null)
            {
                // Pass the player and its index to the selection manager
                PlayerSelectionManager.Instance.ShowPlayerStats(player, playerIndex);
            }
            else
            {
                Debug.LogError("Player data is null in PlayerSlot.");
            }
        }
    }

    public Player GetPlayer()
    {
        return player;
    }
}

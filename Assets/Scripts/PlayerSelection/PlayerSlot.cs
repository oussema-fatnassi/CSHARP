using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
    This class is responsible for managing the player slots in the player selection screen.
    It handles the selection of the player slots and displays the player stats when double-clicked.
*/
public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform iconArea; 
    private GameObject instantiatedPrefab;
    private Player player;  
    public Image image;
    private int playerIndex;  

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
        playerIndex = index;  
    }

    // Called when this slot is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)  
        {
            if (player != null)
            {
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

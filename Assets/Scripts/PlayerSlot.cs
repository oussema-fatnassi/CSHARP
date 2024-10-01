using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform iconArea; // The UI area where the prefab will be instantiated
    private GameObject instantiatedPrefab;
    private Player player;  // Store the Player data
    public Image image;
    public Color selectedColor, notSelectedColor;


    // Method to assign the player prefab and instantiate it in the UI
    public void SetPlayerPrefab(GameObject playerPrefab, Player playerData)
    {
        // Clear the previous prefab
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
        }

        instantiatedPrefab = playerPrefab;
        player = playerData;  // Store the player data for this slot
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            // Notify manager on double-click and show player stats
            PlayerSelectionManager.Instance.ShowPlayerStats(player);
            image.color = selectedColor;
        }
    }
}

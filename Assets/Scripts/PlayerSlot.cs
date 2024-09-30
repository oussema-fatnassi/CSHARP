using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform iconArea; // The UI area where the prefab will be instantiated
    private GameObject instantiatedPrefab;
    private Player player;

    // You can add a public property for the slot name
    public string SlotName => gameObject.name; // Returns the name of the GameObject

    // Method to assign the player prefab and instantiate it in the UI
    public void SetPlayerPrefab(GameObject playerPrefab, Player playerData)
    {
        // Clear the previous prefab
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
        }        
        player = playerData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            PlayerSelectionManager.Instance.ShowPlayerStats(player); // Notify manager on double-click
        }
    }
}

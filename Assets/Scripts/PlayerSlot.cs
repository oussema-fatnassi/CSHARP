using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform iconArea; 
    private GameObject instantiatedPrefab;
    private Player player;  
    public Image image;
    public Color selectedColor, notSelectedColor;

    public string SlotName => gameObject.name;

    public void SetPlayerPrefab(GameObject playerPrefab, Player playerData)
    {
        if (instantiatedPrefab != null)
        {
            Destroy(instantiatedPrefab);
        }

        instantiatedPrefab = playerPrefab;
        player = playerData;  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            if (player != null)
            {
                PlayerSelectionManager.Instance.ShowPlayerStats(player);
                PlayerSelectionManager.Instance.SetSelectedSlot(this); // Notify the manager about the selected slot
                image.color = selectedColor;
            }
            else
            {
                Debug.LogError("Player data is null in PlayerSlot.");
            }
        }
    }

    // Method to reset color to notSelectedColor
    public void ResetColor()
    {
        image.color = notSelectedColor;
    }
}

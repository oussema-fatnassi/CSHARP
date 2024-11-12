using UnityEngine;

/*
    This class is responsible for handling the interaction between the player and the shop.
    It triggers the shop manager to display the shop UI when the player enters the shop trigger area.
*/

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Item item; 
    // Trigger the shop manager when the player enters the shop trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopManager.SetPlayerInRange(true);
            shopManager.SetCurrentItem(item);
        }
    }
    // Set the player out of range status when the player exits the shop trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopManager.SetPlayerInRange(false);
        }
    }
}

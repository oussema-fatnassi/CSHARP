using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Item item; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopManager.SetPlayerInRange(true);
            shopManager.SetCurrentItem(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopManager.SetPlayerInRange(false);
        }
    }
}

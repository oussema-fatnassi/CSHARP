using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shopManager.SetPlayerInRange(true);
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

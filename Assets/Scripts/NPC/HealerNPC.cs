using UnityEngine;

public class HealerNPC : MonoBehaviour
{
    private NPC npc; 
    private Player currentPlayer; 
    void Start()
    {
        npc = GetComponent<NPC>();
    }

    void Update()
    {
        if (npc.playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            FindAndHealPlayer();
        }
    }

    private void FindAndHealPlayer()
    {
        GameObject playerContainer = GameObject.FindWithTag("PlayerContainer");

        if (playerContainer == null || playerContainer.transform.childCount == 0)
        {
            Debug.LogError("No player found in the Player container.");
            return;
        }

        Transform playerTransform = playerContainer.transform.GetChild(0);
        currentPlayer = playerTransform.GetComponent<Player>();

        if (currentPlayer == null)
        {
            Debug.LogError("No Player component found on the active player object.");
            return;
        }

        Debug.Log($"Active Player: {currentPlayer.PlayerName}, Current Health: {currentPlayer.Health}, Max Health: {currentPlayer.MaxHealth}");

        if (currentPlayer.Health < currentPlayer.MaxHealth)
        {
            currentPlayer.Health = currentPlayer.MaxHealth; 
            Debug.Log($"{currentPlayer.PlayerName} has been healed to max health. New Health: {currentPlayer.Health}");
        }
        else
        {
            Debug.Log("Player already has max health.");
        }
    }
}
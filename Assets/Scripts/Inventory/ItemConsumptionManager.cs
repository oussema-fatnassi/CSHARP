using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConsumptionManager : MonoBehaviour
{
    public static ItemConsumptionManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConsumeItem(Player player, Item item)
{
    if (player == null || item == null)
    {
        Debug.LogError("Player or Item is null in ItemConsumptionManager.");
        return;
    }

    Debug.Log($"Applying {item.name} to {player.PlayerName}");

    player.Health += item.value;  
    Debug.Log($"{player.PlayerName}'s new health: {player.Health}");

}

}
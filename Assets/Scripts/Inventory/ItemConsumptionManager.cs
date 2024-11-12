using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for managing the consumption of items by the player.
    It applies the effects of the items to the player's stats.
*/

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
    // Consumes the item and applies its effects to the player
    public void ConsumeItem(Player player, Item item)
    {
        if (player == null || item == null)
        {
            Debug.LogError("Player or Item is null in ItemConsumptionManager.");
            return;
        }

        Debug.Log($"Applying {item.name} to {player.PlayerName}");

        switch (item.type)
        {
            case ConsumableType.Health:
                player.Health = Mathf.Min(player.Health + item.value, player.MaxHealth);
                Debug.Log($"{player.PlayerName}'s new health: {player.Health}");
                break;

            case ConsumableType.Mana:
                player.Mana = Mathf.Min(player.Mana + item.value, player.MaxMana);
                Debug.Log($"{player.PlayerName}'s new mana: {player.Mana}");
                break;

            case ConsumableType.Defense:
                player.Defense += item.value;
                Debug.Log($"{player.PlayerName}'s new defense: {player.Defense}");
                break;

            case ConsumableType.Attack:
                player.Damage += item.value;
                Debug.Log($"{player.PlayerName}'s new attack damage: {player.Damage}");
                break;

            case ConsumableType.Speed:
                player.Speed += item.value;
                Debug.Log($"{player.PlayerName}'s new speed: {player.Speed}");
                break;

            case ConsumableType.Precision:
                player.Precision += item.value;
                Debug.Log($"{player.PlayerName}'s new precision: {player.Precision}");
                break;

            case ConsumableType.Intelligence:
                player.Intelligence += item.value;
                Debug.Log($"{player.PlayerName}'s new intelligence: {player.Intelligence}");
                break;

            case ConsumableType.Level:
                player.Level += item.value;
                Debug.Log($"{player.PlayerName}'s new level: {player.Level}");
                break;

            default:
                Debug.LogWarning("Unknown consumable type. No effect applied.");
                break;
        }
    }
}

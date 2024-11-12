using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for setting up the battle unit.
*/

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] BattleHud hud;
    [SerializeField] bool isPlayerUnit;

    public PlayerStats PlayerStats { get => playerStats; }

    // Set the player stats
    public void Setup()
    {
        hud.SetData(playerStats);
        if (isPlayerUnit)
        {
            // GetComponent<BattlePlayer>().sprite = playerStats.Sprite;
            playerStats.health = playerStats.maxHealth;
            playerStats.mana = playerStats.maxMana;
        }
    }
}

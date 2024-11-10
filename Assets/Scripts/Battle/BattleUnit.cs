using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] BattleHud hud;
    [SerializeField] bool isPlayerUnit;

    public PlayerStats PlayerStats { get => playerStats; }

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

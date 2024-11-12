using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is used to setup the battle system.
    It sets up the player unit and the enemy unit.
    It also sets up the player HUD and the enemy HUD.
*/

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    // [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    // [SerializeField] BattleHud enemyHud;

    private void Start() 
    {
        SetupBattle();
    }

    // This function sets up the battle system.
    public void SetupBattle()
    {
        playerUnit.Setup();
        // enemyUnit.Setup();
        playerHud.SetData(playerUnit.PlayerStats);
    }
}

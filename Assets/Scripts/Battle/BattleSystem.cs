using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    // [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;

    // private void Start() 
    // {
    //     SetupBattle();
    // }

    // public void SetupBattle()
    // {
    //     playerUnit.Setup();
    //     // enemyUnit.Setup();
    //     playerHud.SetData(playerUnit.PlayerStats);
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject BattleSystemObject;
    [SerializeField] GameObject InventoryButton;
    [SerializeField] GameObject PlayerStatsButton;
    [SerializeField] GameObject Money;
    public void moveEnemies ()
    {
        BattleSystemObject.SetActive(false);
        InventoryButton.SetActive(true);
        PlayerStatsButton.SetActive(true);
        Money.SetActive(true);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyPathfinding enemyPathfinding = enemy.GetComponent<EnemyPathfinding>();
            if (enemyPathfinding != null)
            {
                enemyPathfinding.enabled = true;
            }
        }
    }
}

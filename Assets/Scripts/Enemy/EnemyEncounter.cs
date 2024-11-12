using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is attached to the EnemyEncounter prefab.
    It is used to detect when the player enters the enemy's range and triggers a battle.
    It also destroys the enemy encounter object after a delay.
*/

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] GameObject BattleSystemObject;
    [SerializeField] GameObject InventoryButton;
    [SerializeField] GameObject PlayerStatsButton;
    [SerializeField] GameObject Money;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range, autodesctructing in 3 2 1...");
            StartCoroutine(DelayAndDeactivate(2f));
            BattleSystemObject.SetActive(true);
            InventoryButton.SetActive(false);
            PlayerStatsButton.SetActive(false);
            Money.SetActive(false);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyPathfinding enemyPathfinding = enemy.GetComponent<EnemyPathfinding>();
                if (enemyPathfinding != null)
                {
                    enemyPathfinding.enabled = false;
                }
            }
        }
    }

    private IEnumerator DelayAndDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

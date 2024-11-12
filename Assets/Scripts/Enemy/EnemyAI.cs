using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is responsible for the enemy's AI behavior.
    The enemy will roam around the map, moving to random positions.
    The enemy will move to a new random position every 2 seconds.
*/

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    // Start is called before the first frame update
    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    // Coroutine to handle the enemy's roaming behavior
    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    // Get a random position for the enemy to roam to
    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}

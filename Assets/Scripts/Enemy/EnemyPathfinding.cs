using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    This script is attached to the Enemy prefab.
    It is used to move the enemy towards the player.
    It also handles collision with the MonsterBlock layer to stop the enemy's movement.
*/

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Vector2 moveDir;

    // Awake is called when the script instance is being loaded
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    // Move the monster towards the target position
    public void MoveTo(Vector2 targetPosition) {
        moveDir = targetPosition;
    }

    // Handle collision with the MonsterBlock layer
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MonsterBlock")) {
            StopMovingOrChangeDirection();
        }
    }

    // Stop the monster or change its direction
    private void StopMovingOrChangeDirection() {
        moveDir = Vector2.zero;

        Vector2 newDirection = -moveDir;
        MoveTo(newDirection);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition) {
        moveDir = targetPosition;
    }

    // Handle collision with the MonsterBlock layer
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("MonsterBlock")) {
            // The monster collided with an invisible enclosure
            // Here, we stop the monster's movement or make it turn around
            StopMovingOrChangeDirection();
        }
    }

    // Stop the movement or change the direction of the monster
    private void StopMovingOrChangeDirection() {
        // To stop the monster, set the move direction to zero
        moveDir = Vector2.zero;

        // Alternatively, to change direction, generate a new roam position:
        Vector2 newDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        MoveTo(newDirection); // Change direction to roam somewhere else
    }
}

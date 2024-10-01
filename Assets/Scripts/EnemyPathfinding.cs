using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string[] dialogue;

    private int index;
    public float wordSpeed;
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

        if (collision.CompareTag("Player")) {
            // The monster collided with the player
            // Here, we stop the monster's movement or make it turn around
            // Display dialogue panel
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
            StartCoroutine(WaitForNextLine());
            Destroy(gameObject);
        }
    }

    // Stop the movement or change the direction of the monster
    private void StopMovingOrChangeDirection() {
        // To stop the monster, set the move direction to zero
        moveDir = Vector2.zero;

        // Alternatively, to change direction, generate a new roam position:
        Vector2 newDirection = -moveDir; // Reverse the current direction to go back the way it came from
        MoveTo(newDirection); // Change direction to roam somewhere else
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public IEnumerator WaitForNextLine()
    {
        while (true)
        {
            if (dialogueText.text == dialogue[index])
            {
                if (index < dialogue.Length - 1)
                {
                    index++;
                    dialogueText.text = "";
                    StartCoroutine(Typing());
                }
                else
                {
                    dialogueText.text = "";
                    index = 0;
                }
            }
            yield return null;
        }
    }
}

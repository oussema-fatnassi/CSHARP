using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for controlling the player character in the game.
    It handles the movement of the player character and the animation of the player character.
*/

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float horizontal;
    private float vertical;

    public bool IsTesting { get; set; } = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        Animate();
    }
    // Move the player character based on the input
    private void Move()
    {
        horizontal = IsTesting ? MockInput.GetAxis("Horizontal") : Input.GetAxis("Horizontal");
        vertical = IsTesting ? MockInput.GetAxis("Vertical") : Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += speed * Time.deltaTime * movement;
    }
    // Animate the player character based on the movement
    private void Animate()
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

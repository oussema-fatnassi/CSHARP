using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private float horizontal;
    private float vertical;

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

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Créer un vecteur de mouvement
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Normaliser le vecteur de mouvement si nécessaire pour conserver une vitesse constante
        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        // Flip player sprite based on movement direction
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Appliquer le mouvement à la position du joueur
        transform.position += speed * Time.deltaTime * movement;
    }


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

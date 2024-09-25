using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField] Transform leader; // Référence au personnage "Dragon"
    public float followDistance = 2f; // Distance à maintenir entre "Dwarf" et "Dragon"
    public float followSpeed = 5f; // Vitesse à laquelle "Dwarf" suit "Dragon"
    
    private Animator animator;
    private Vector3 lastPosition;

    private void Start()
    {
        // Récupérer l'Animator
        animator = GetComponent<Animator>();
        // Initialiser la dernière position à la position actuelle
        lastPosition = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, leader.position);

        if (distance > followDistance)
        {
            // Calculer la direction à suivre
            Vector3 direction = (leader.position - transform.position).normalized;

            // Appliquer le mouvement normalisé pour garder une vitesse constante
            transform.position += direction * followSpeed * Time.deltaTime;

            // Gestion du flip horizontal seulement
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Animer "Dwarf"
            AnimateMovement(direction);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    private void AnimateMovement(Vector3 direction)
    {
        // Si "Dwarf" est en mouvement, activer l'animation de marche
        if (direction != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}

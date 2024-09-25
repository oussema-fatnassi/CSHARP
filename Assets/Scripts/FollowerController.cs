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
        // Calculer la distance entre "Dwarf" et "Dragon"
        float distance = Vector3.Distance(transform.position, leader.position);

        // Si la distance dépasse la valeur définie, "Dwarf" se déplace vers "Dragon"
        if (distance > followDistance)
        {
            // Calcul de la direction à suivre
            Vector3 direction = (leader.position - transform.position).normalized;

            // Déplacement de "Dwarf" vers "Dragon"
            transform.position += direction * followSpeed * Time.deltaTime;

            // Flip sprite en fonction de la direction horizontale
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
            // Si "Dwarf" ne bouge pas, désactiver l'animation de marche
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

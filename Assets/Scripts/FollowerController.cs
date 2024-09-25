using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField] private Transform leader; // Le leader initial (peut être configuré dans l'inspecteur)
    public float followDistance = 2f;
    public float followSpeed = 5f;

    private Animator animator;
    private Vector3 lastPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position; // Initialiser la dernière position
    }

    private void Update()
    {
        if (leader != null) // S'assurer que le leader est bien défini
        {
            FollowLeader();
        }
    }

    private void FollowLeader()
    {
        float distance = Vector3.Distance(transform.position, leader.position);

        if (distance > followDistance)
        {
            // Déplacement vers le leader
            Vector3 direction = (leader.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;

            // Gestion du flip horizontal
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Animation du mouvement
            AnimateMovement(direction);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void AnimateMovement(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void SetLeader(Transform newLeader)
    {
        leader = newLeader; // Permet de définir dynamiquement un nouveau leader
    }
}

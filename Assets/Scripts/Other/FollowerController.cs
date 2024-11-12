using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class is responsible for controlling the movement of the follower character.
    It makes the follower character follow the leader character at a certain distance.
*/

public class FollowerController : MonoBehaviour
{
    [SerializeField] Transform leader; 
    public float followDistance = 2f; 
    public float followSpeed = 5f; 
    
    private Animator animator;
    private Vector3 lastPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }
    // Set the direction of the follower character based on the leader character's position
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, leader.position);

        if (distance > followDistance)
        {
            Vector3 direction = (leader.position - transform.position).normalized;

            transform.position += direction * followSpeed * Time.deltaTime;

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

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
}

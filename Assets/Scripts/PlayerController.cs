using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField] 
    private float speed = 1;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float horizontal;
    private float vertical;

    // Flag to indicate if we are running tests
    public bool IsTesting { get; set; } = false; // Default is false

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
        // Use MockInput if we are in testing mode, otherwise use Unity's Input
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

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
}

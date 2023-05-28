using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform checkpoint1;
    [SerializeField] private Transform checkpoint2;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float detectionDistance = 2f;

    private Transform targetCheckpoint;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isDisabled = false;
    public PlayerAbility isAbilityScript;

    // Start is called before the first frame update
    void Start()
    {
        targetCheckpoint = checkpoint2;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAbilityScript.isAbilityActive)
        {
            // Check if player is within detection range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionDistance);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Disable the enemy and play the "DogSleeping" animation
                    DisableEnemy();
                    return;
                }
            }
        }


        // Move towards the current target checkpoint
        Vector2 direction = targetCheckpoint.position - transform.position;
        rb.velocity = direction.normalized * moveSpeed;

        // Flip the sprite if moving to the left
        if (direction.x < 0f)
        {
            sprite.flipX = true;
        }
        else if (direction.x > 0f)
        {
            sprite.flipX = false;
        }

        // Check if we've reached the target checkpoint and switch to the other one
        if (Vector2.Distance(transform.position, targetCheckpoint.position) < 0.1f)
        {
            if (targetCheckpoint == checkpoint1)
            {
                targetCheckpoint = checkpoint2;
            }
            else
            {
                targetCheckpoint = checkpoint1;
            }
        }
    }

    private void DisableEnemy()
    {
        isDisabled = true;
        rb.velocity = Vector2.zero; // Stop the enemy's movement
        sprite.flipX = false; // Reset the sprite flip if needed

        // Play the "DogSleeping" animation
        anim.SetBool("DogSleeping", true);

        // Disable the enemy's collider and renderer
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Hide the checkpoints
        // Freeze the X position of the enemy
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float abilityDuration = 10f;
    public float abilityRadius = 5f;
    public LayerMask enemyLayer;
    public Animator animator;

    public bool isAbilityActive = false;
    private float abilityTimer = 0f;
    private ManaBar manaBar; // Reference to the ManaBar component

    private float manaReductionInterval = 5f; // Time interval for mana reduction
    private float lastManaReductionTime = 0f; // Timestamp of the last mana reduction

    private void Start()
    {
        manaBar = FindObjectOfType<ManaBar>();
    }

    private void Update()
    {
        animator.SetBool("playerAbility", isAbilityActive);

        if (Input.GetMouseButtonDown(1) && !isAbilityActive && CheckManaAvailability())
        {
            ActivateAbility();
        }

        if (Input.GetMouseButtonUp(1) && isAbilityActive)
        {
            DeactivateAbility();
        }

        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;

            if (abilityTimer <= 0f)
            {
                animator.SetBool("playerAbility", false);
                DeactivateAbility();
            }
            else
            {
                // Check if it's time to reduce mana
                if (Time.time - lastManaReductionTime >= manaReductionInterval)
                {
                    ReduceMana(0.2f); // Reduce mana by 20%
                    lastManaReductionTime = Time.time; // Update the last mana reduction time
                }
            }
        }
    }

    private void ActivateAbility()
    {
        isAbilityActive = true;
        abilityTimer = abilityDuration;

        // Activate the children of the player GameObject
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        // Play the PlayerAbility animation
        animator.SetBool("playerAbility", true);

        // Disable collisions with enemies in the ability zone
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, abilityRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void DeactivateAbility()
    {
        isAbilityActive = false;

        // Deactivate the children of the player GameObject
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        // Reset the PlayerAbility animation
        animator.SetBool("playerAbility", false);

        // Enable collisions with enemies in the ability zone
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, abilityRadius, enemyLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    private bool CheckManaAvailability()
    {
        // Check if the player has enough mana to activate the ability
        // Return true if mana is sufficient, false otherwise
        return manaBar != null && manaBar.CurrentMana >= 10f; // Adjust the mana threshold as needed
    }

    private void ReduceMana(float reductionPercentage)
    {
        float currentMana = manaBar.CurrentMana;
        float reductionAmount = currentMana * reductionPercentage;
        manaBar.UseMana(reductionAmount);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a visual representation of the ability zone in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, abilityRadius);
    }
}

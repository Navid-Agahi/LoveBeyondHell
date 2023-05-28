using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private UIManager uiManager; // Reference to the UIManager script
    private int deathCounter;
    private Vector3 startingPosition;
    [SerializeField] private GameObject gameoverCanvas;
    private Vector2 CheckPointPos;

    [SerializeField] private AudioSource deathSoundEffect;
    

    // Number of deaths allowed before game over
    private int maxDeaths = 2;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager script in the scenes

        // Save the starting position of the player
        startingPosition = transform.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
    public void UpdateCheckPoint(Vector2 pos)
    {
        CheckPointPos = pos;
    }

    private void Die()
    {
        deathSoundEffect.Play();
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;

        if (uiManager != null)
        {
            uiManager.IncrementDeathCounter(); // Call the IncrementDeathCounter method in the UIManager script
        }

        deathCounter++;

        if (deathCounter <= maxDeaths)
        {
            // Respawn the player
            Invoke("Respawn", 4f);
        }
        else
        {
            gameoverCanvas.SetActive(true);
            // Restart the game
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    private void Respawn()
    {
        // Reset the position of the player to the starting position
        transform.position = CheckPointPos;

        // Reset the rigidbody type to dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    [SerializeField] private bool isRespawnable = true;
    [SerializeField] private float respawnDelay = 2f;
    private Vector2 spawnPosition;
    private Rigidbody2D rb;
    void Start()
    {
        spawnPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        if (isRespawnable)
        {
            Invoke("Respawn", respawnDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Respawn()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
        SendMessage("resetHP",SendMessageOptions.DontRequireReceiver);
    }
}

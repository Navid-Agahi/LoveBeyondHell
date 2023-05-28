using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    public Transform destination;
    public float delay = 3f;

    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    private IEnumerator Teleport(GameObject player)
    {
        canTeleport = false;

        player.SetActive(false);
        yield return new WaitForSeconds(delay);
        player.transform.position = destination.position;
        player.SetActive(true);

        // Set canTeleport flag to true after a short delay to avoid triggering the OnTriggerEnter2D method
        // again immediately after teleporting the player.
        yield return new WaitForSeconds(0.1f);
        canTeleport = true;
    }
}

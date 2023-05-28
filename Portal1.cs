using UnityEngine;

public class Portal1 : MonoBehaviour
{
    public GameObject otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.transform.position = otherPortal.transform.position;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

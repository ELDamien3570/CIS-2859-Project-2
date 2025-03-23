using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // Debug.Log("Player Collision Detected");
            collision.gameObject.GetComponent<PlayerController>().CoinPickup();
            Destroy(gameObject);
            Destroy(circleCollider);
        }
    }
}

using UnityEngine;

public class DeathBoxCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Death Detected");
            collision.gameObject.GetComponent<PlayerController>().Death();
            Destroy(gameObject);
        }
    }
}

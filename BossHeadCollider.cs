using UnityEngine;

public class BossHeadCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y && collision.gameObject.CompareTag("Player"))
        {
            this.GetComponentInParent<BossController>().TakeHit();
            Debug.Log("Ai Hit");
        }
    }
}

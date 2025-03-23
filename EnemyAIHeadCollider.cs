using UnityEngine;

public class EnemyAIHeadCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y && collision.gameObject.CompareTag("Player"))
        {
            this.GetComponentInParent<EnemyAIController>().AiDeath();
            //Debug.Log("Ai Hit");
        }
    }
}

using System.Collections;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [Header("Gane Objects")]
    public Transform[] wayPoints;
    private Rigidbody2D aIRigidbody;
    [SerializeField]
    private GameManager gameManager;

    [Header("AI Stats")]
    public float speed;
    public int startingPoint;
    private int i;
    private bool dead = false;

    private void Start()
    {
        transform.position = wayPoints[startingPoint].position;
        aIRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!dead)
        {
            AIMovement();
            
        }
        else
            aIRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void AIMovement()
    {
        if (Vector2.Distance(transform.position, wayPoints[i].position) < 0.05f)
        {
            i++;
            if (i == wayPoints.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dead)
            {
                collision.gameObject.GetComponent<PlayerController>().Death();
                //Debug.Log("Player Hit");
            }
            else
                return;
        }
    }

    public void AiDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        dead = true;
        gameManager.EnemyKilled();
        StartCoroutine(AiDeathTimer());
    }

    IEnumerator AiDeathTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}

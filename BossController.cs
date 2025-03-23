using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    GameObject hitEffect;
    [SerializeField]
    GameManager gameManager;

    [Header("Movement")]
    public Transform[] wayPoints;
    public Transform[] jumpPoints;
    private Rigidbody2D aIRigidbody;
    public float baseSpeed;
    private float randomizedSpeed;
    public int startingPoint;
    private int i;

    [Header("Bot Info")]
    [SerializeField]
    private int health = 3;
    private float randomMovementOffset;
    public float jumpSpeed = 2f;

    [Header("Bot Behavior")]
    private bool pausedFromHit = false;
    private float jumpDecider;
    public bool jumping = false;
    public bool canJump = true;


    private void Start()
    {
        transform.position = wayPoints[startingPoint].position;
        aIRigidbody = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        HandleAllMovement();
    
    }
    private void HandleAllMovement()
    {
        randomMovementOffset = Random.Range(0.2f, 1.5f);
        randomizedSpeed = Random.Range(baseSpeed - (baseSpeed/2), baseSpeed +  (baseSpeed/2));
        jumpDecider = Random.Range(0f, 50f);
        if (jumpDecider < .1f && canJump)
        {
            StartCoroutine(JumpTimer());
        }
        if (health > 0 && !pausedFromHit)
        {
            AIMovement();
        }
        else if (health <= 0)
        {
            aIRigidbody.bodyType = RigidbodyType2D.Dynamic;
            AiDeath();
        }
        else
        {
            return;
        }
    }

    private void AIMovement()
    {    
        if (!jumping)
        {
            if (Vector2.Distance(transform.position, wayPoints[i].position) < randomMovementOffset)
            {
                i++;
                if (i >= wayPoints.Length)
                {
                    i = Random.Range(0, wayPoints.Length);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[i].position, randomizedSpeed * Time.deltaTime);
        }
        else if (jumping)
        {
            if (Vector2.Distance(transform.position, jumpPoints[i].position) < 0.2f)
            {
                i++;
                if (i >= jumpPoints.Length)
                {
                    i = Random.Range(0, jumpPoints.Length);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, jumpPoints[i].position, jumpSpeed * Time.deltaTime);
        }

    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!pausedFromHit)
            {
                collision.gameObject.GetComponent<PlayerController>().Death();
                //Debug.Log("Player Hit");
            }
            else
                return;
        }
    }

    
    public void TakeHit()
    {
        if (!pausedFromHit)
        {
            pausedFromHit = true;
            hitEffect.SetActive(true);
            StartCoroutine(HitReset());
            health--;
        }
        else
        {
            return;
        }
    }
    private void AiDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        gameManager.BossKilled();
        StartCoroutine(AiDeathTimer());
    }

    IEnumerator AiDeathTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    IEnumerator HitReset()
    {    
        yield return new WaitForSeconds(3);
        pausedFromHit = false;
        hitEffect.SetActive(false);
    }
    IEnumerator JumpTimer()
    {
        jumping = true;
            yield return new WaitForSeconds(0.5f);
        jumping = false;
        canJump = false;
        StartCoroutine(JumpReset());
    }

    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(10f);
        canJump = true;
    }
}

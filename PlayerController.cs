using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D plRigid;
    [SerializeField]
    private AnimationManager animationManager;
    [SerializeField]
    private CoinCounterScript coinCounterScript;
    [SerializeField]
    private GameManager sceneManager;
    private BoxCollider2D boxCollider;

    [Header("Movement")]
    public float speed = 4.5f;   
    public float jumpHeight = 2f;
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    public bool jumpReady = true;

    [Header ("Raycasting")]
    public LayerMask platformLayer;
    public Vector2 castSize;
    public float castDistance;

    [Header("Timers")]
    public float jumpTimerFloat;

    [Header("Player Stats")]
    public int health = 1;   
    public int coins = 0;
    public bool death = false;

    [Header("Troubleshooting")]
    [SerializeField]
    private UIManager uiManager;

    void Start()
    {
        plRigid = GetComponent<Rigidbody2D>();  
        animationManager = GetComponent<AnimationManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    void FixedUpdate()
    {
        if (!death && sceneManager.bossAlive == true)
        {
            HandleAllMovement();
        }
        else
            return;

        if (coins >= 10)
            LevelOneComplete();
    }

    private void HandleAllMovement()
    {
        Movement();
        IsGrounded();
        if (Input.GetKey(jumpKey) && jumpReady)
        {
            Jump();
        }
    }
    private void Movement()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, plRigid.linearVelocity.y);
        plRigid.linearVelocity = movement;
        animationManager.UpdateAnimatorValues(movement.x, movement.y);

        if (movement.x == 0)
        {
            animationManager.animator.SetBool("IsIdle", true);
        }
        else
        {
            animationManager.animator.SetBool("IsIdle", false);
        }

        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
   
    private void Jump()
    {
        uiManager.ShowJumpIndicator();
        plRigid.AddForce(new Vector2(plRigid.linearVelocity.y, jumpHeight));
        animationManager.PlayTargetAnimation("Jump", true);
        animationManager.animator.SetBool("IsJumping", true);
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, castSize,  0, -transform.up, castDistance, platformLayer))
        {
            if (!jumpReady)
            {
                StartCoroutine(JumpTimer());
            }
            animationManager.animator.SetBool("IsJumping", false);
           //Debug.Log("Grounded");
            return true;                                
        }
        else
        {            
           // Debug.Log("Not Grounded");           
            StartCoroutine(JumpTimerNegative());
            return false;        
        }
    }    

    public void CoinPickup()
    {
        coins++;
       // Debug.Log("Coin Picked Up");
        coinCounterScript.coinCount++;
    }

    public void Death()
    {
        death = true;
        boxCollider.isTrigger = true;
    }

    public void LevelOneComplete()
    {
        sceneManager.ShowLevelTwoRiser();
    }

    IEnumerator JumpTimer()
    {
        if (jumpReady)
        {
            yield return new WaitForSeconds(0f);
        }
        else if (!jumpReady)
        {           
            yield return new WaitForSeconds(jumpTimerFloat);
            jumpReady = true;
        }
    }

    IEnumerator JumpTimerNegative()
    {
        if (jumpReady)
        {
            yield return new WaitForSeconds(jumpTimerFloat);
            jumpReady = false;
        }
        else if (!jumpReady)
        {  
            yield return new WaitForSeconds(0f);
        }
    }
}

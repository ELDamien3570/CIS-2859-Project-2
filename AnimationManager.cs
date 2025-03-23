using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    int horizontal;
    int vertical;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isGrounded)
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horzontalMovement, float verticalMovement)
    {
        animator.SetFloat(horizontal, horzontalMovement);
        animator.SetFloat(vertical, verticalMovement);
    }
}

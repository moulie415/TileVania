using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    float gravityScaleAtStart;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
       moveInput = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if (value.isPressed && capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rb.linearVelocity.y);
        rb.linearVelocity = playerVelocity;
        
        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon);
    }
    
    void FlipSprite()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon) {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1f);
        }
    }
    
    void ClimbLadder()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * climbSpeed);
        rb.linearVelocity = climbVelocity;
        rb.gravityScale = 0f;
        animator.SetBool("isClimbing", true);
        animator.SetFloat("climbSpeed", Mathf.Abs(moveInput.y));
    }
}

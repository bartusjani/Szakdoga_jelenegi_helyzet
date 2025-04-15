
using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    private float horizontal;
    public float speed = 5f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;
    bool isGrounded = true;
    
    public Animator animator;

    public PlayerStamina playerStamina;
    private float staminaUseInterval=2f;
    private float staminaUseTimer=0f;

    private bool canDash=true;
    private bool isDashing;
    private float dashingPower = 400f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;

    float runStamina = 10f;
    float dashStamina = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");

        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            isGrounded = false;
            

            animator.SetBool("isJumping",true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            
        }

        if(Input.GetKeyDown(KeyCode.Space) && canDash&& playerStamina.stamina > dashStamina)
        {  
            StartCoroutine(Dash());
        }
        else if(Input.GetKeyDown(KeyCode.Space) && canDash && !isGrounded==false && playerStamina.stamina >dashStamina)
        {
            StartCoroutine(JumpDash());
        }

       

        if (Input.GetKey(KeyCode.S) && IsGrounded()&& playerStamina.stamina > runStamina)
        {
            Run();
            staminaUseTimer += Time.deltaTime;
            if (staminaUseTimer >= staminaUseInterval)
            {
                if (playerStamina != null)
                {
                    playerStamina.TakeStamina(1);
                }
                staminaUseTimer = 0f;
            }
        }
        else
        {
            speed = 5f;
            animator.SetBool("isRunning", false);
        }

        Flip();
        
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Run()
    {
        animator.SetBool("isRunning", true);
        speed = 10f;
    }
    private IEnumerator Dash()
    {

        animator.SetBool("isDashing", canDash);
        canDash = false;
        isDashing = true;
        playerStamina.TakeStamina(10);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
        yield return new WaitForSeconds(dashingTime);
        
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", canDash);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator JumpDash()
    {
        animator.SetBool("isDashing", canDash);
        animator.SetBool("isJumping", true);
        canDash = false;
        isDashing = true;
        playerStamina.TakeStamina(15);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        
        yield return new WaitForSeconds(dashingTime);
        
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isDashing", canDash);
        animator.SetBool("isJumping", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);


        

    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

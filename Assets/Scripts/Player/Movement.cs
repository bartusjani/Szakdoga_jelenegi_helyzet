
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
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] private float enemySlideForce = 10f;
    [SerializeField] private AudioClip[] walkClip;

    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip jumpClip;

    float runStamina = 10f;
    float dashStamina = 20f;

    private float footstepTimer=0f;
    private float footstepInterval=0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        CheckEnemyBelow();
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
            AudioManager.instance.PlaySound(jumpClip, transform, 1f);
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

       

        if (Input.GetKey(KeyCode.S) && IsGrounded()&& playerStamina.stamina > runStamina && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
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
        //Debug.Log(Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer));
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
        AudioManager.instance.PlaySound(dashClip, transform, 1f);
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
    private void CheckEnemyBelow()
    {

        Vector2 origin = groundCheck != null ? groundCheck.position : (Vector2)transform.position;
        float rayLength = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, rayLength, enemyLayer);

        if (hit.collider != null && rb.linearVelocity.y <= 0f)
        {
            float slideDir = rb.linearVelocity.x != 0 ? Mathf.Sign(rb.linearVelocity.x) : (isFacingRight ? 1 : -1);


            Vector2 slideForce = new Vector2(slideDir * 600000000f, 1000f);
            rb.AddForce(slideForce, ForceMode2D.Impulse);

            Debug.Log("Csúszik az enemy-rõl le, irány: " + slideDir);
        }
    }


    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        float yVel = rb.linearVelocity.y;
        float yThreshold = 0.1f;
        float filteredYVelocity = Mathf.Abs(yVel) < yThreshold ? 0f : yVel;

        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", filteredYVelocity);

        if (Mathf.Abs(horizontal) > 0.1f && IsGrounded() && !isDashing)
        {
            footstepTimer += Time.fixedDeltaTime;
            if (footstepTimer >= footstepInterval)
            {
                
                AudioManager.instance.PlayRandomSound(walkClip,transform,1f);
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
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

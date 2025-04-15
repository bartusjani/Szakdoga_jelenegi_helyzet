using System.Collections;
using UnityEngine;


public class PlayerLedgeGrab : MonoBehaviour
{

    public float climbingHorizontalOffset;

    
    private Vector2 topOfPlayer;
    
    private GameObject ledge;
    
    private float animationTime = .5f;
    private bool falling;
    private bool moved;
    private bool climbing;

    
    private bool GrabbingLedge;
    private Collider2D col;
    private Rigidbody2D rb;
    Animator animator;

    
    private void Start()
    {
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    

    

    protected  void Update()
    {
        CheckForLedge();
        LedgeHanging();
        if (GrabbingLedge || climbing)
        {
            StopHorizontalMovement();
        }

    }

    protected void CheckForLedge()
    {

            if (transform.localScale.x > 0)
            {
                topOfPlayer = new Vector2(col.bounds.max.x + .1f, col.bounds.max.y);
                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.right, .2f);
                if (!GrabbingLedge&&hit && hit.collider.gameObject.GetComponent<Ledge>())
                {

                    ledge = hit.collider.gameObject;
                    Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                    if (col.bounds.min.x < ledgeCollider.bounds.min.x)
                    {
                        GrabbingLedge = true;
                    }
                }
            }

            else
            {

                topOfPlayer = new Vector2(col.bounds.min.x - .1f, col.bounds.max.y);

                RaycastHit2D hit = Physics2D.Raycast(topOfPlayer, Vector2.left, .2f);

                if (!GrabbingLedge && hit && hit.collider.gameObject.GetComponent<Ledge>())
                {

                    ledge = hit.collider.gameObject;

                    Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
                    //col.bounds.max.y < ledgeCollider.bounds.max.y &&
                    if (col.bounds.max.y > ledgeCollider.bounds.center.y && col.bounds.max.x > ledgeCollider.bounds.max.x)
                    {


                        GrabbingLedge = true;
                    }
                }
            }

            if (ledge != null && GrabbingLedge)
            {
                StopHorizontalMovement();
                AdjustPlayerPosition();
                rb.linearVelocity = Vector2.zero;
                animator.SetBool("isGrabbingLedge", true);
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                animator.SetBool("isGrabbingLedge", false);
                rb.bodyType = RigidbodyType2D.Dynamic;
                StopHorizontalMovement();
            }
        
        
    }

    
    protected virtual void LedgeHanging()
    {
        
        if (GrabbingLedge && Input.GetAxis("Vertical") > 0 && !climbing)
        {
            StopHorizontalMovement();
            climbing = true;
            
            if (transform.localScale.x > 0)
            {
                
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x + climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + col.bounds.extents.y), animationTime));
            }
            
            else
            {
                
                StartCoroutine(ClimbingLedge(new Vector2(transform.position.x - climbingHorizontalOffset, ledge.GetComponent<Collider2D>().bounds.max.y + col.bounds.extents.y), animationTime));
            }
        }
        
        if (GrabbingLedge && Input.GetAxis("Vertical") < 0)
        {
            
            ledge = null;

            moved = false;

            GrabbingLedge = false;

            falling = true;

            rb.bodyType = RigidbodyType2D.Dynamic;

            Invoke("NotFalling", .5f);
            StopHorizontalMovement();
        }
    }

    protected virtual IEnumerator ClimbingLedge(Vector2 topOfPlatform, float duration)
    {
        animator.SetBool("isClimbing", climbing);
        float time = 0;
        Vector2 startValue = transform.position;
        while (time <= duration)
        {
            transform.position = Vector2.Lerp(startValue, topOfPlatform, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        ledge = null;
        moved = false;
        climbing = false;
        animator.SetBool("isClimbing", climbing);
        GrabbingLedge = false;
        
    }

    protected virtual void AdjustPlayerPosition()
    {
        if (!moved)
        {
            moved = true;
            Collider2D ledgeCollider = ledge.GetComponent<Collider2D>();
            Ledge platform = ledge.GetComponent<Ledge>();
            if (transform.localScale.x > 0)
            {
                transform.position = new Vector2((ledgeCollider.bounds.min.x - col.bounds.extents.x) + platform.hangingHorizontalOffset, (ledgeCollider.bounds.max.y - col.bounds.extents.y - .5f) + platform.hangingVerticalOffset);
            }
            else
            {
                transform.position = new Vector2((ledgeCollider.bounds.max.x + col.bounds.extents.x) - platform.hangingHorizontalOffset, (ledgeCollider.bounds.max.y - col.bounds.extents.y - .5f) + platform.hangingVerticalOffset);
            }
        }
    }
    void StopHorizontalMovement()
    {
        if (GrabbingLedge || climbing)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            rb.angularVelocity = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    protected virtual void NotFalling()
    {
        falling = false;
        
    }
}
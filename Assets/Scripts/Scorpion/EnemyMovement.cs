using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    public Transform[] patrolPoints;
    public int patrolDestination=0;

    public float speed=2f;
    public Transform groundCheck;

    public Transform player;

    public bool isChasing=false;
    public float chaseDistance=2f;

    public float stopDis = 2f;
    Vector2 moveDir;
    RaycastHit2D groundInfo;
    bool facingRight = true;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        FlipTowardsPlayer();
        //RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);

        animator.SetBool("isWalking", true);

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < chaseDistance)
        {
            isChasing = true;
        }
        if (isChasing)
        {
            if (groundInfo.collider == false) 
            {
                isChasing = false;
                Patroling();

            }

            TargetingPlayer();   
        }
        else
        {
            Patroling();
            
        }
    }

    private void FixedUpdate()
    {
        groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);
        if (rb.linearVelocity.y == 0) rb.linearVelocity = moveDir * speed;
    }
    public void Patroling()
    {
        Transform targetPoint = patrolPoints[patrolDestination];
        moveDir = (targetPoint.position - transform.position).normalized;

        if ((moveDir.x < 0 && facingRight) || (moveDir.x > 0 && !facingRight))
        {
            Flip();
        }

        switch (patrolDestination)
        {
            case 0:
                if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
                {
                    patrolDestination = 1;
                }
                break;
            case 1:
                if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
                {
                    patrolDestination = 0;
                }
                break;
        }
    }

    public void TargetingPlayer()
    {
        if (player == null) return;
        //Debug.Log("Targetel");
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer > stopDis)
        {
            moveDir = (player.position - transform.position).normalized;
        }
        else
        {
            moveDir = Vector2.zero;
            
        }
    }
    public void FlipTowardsPlayer()
    {
        if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newS = transform.localScale;
        newS.x *= -1;
        transform.localScale = newS;
    }
}

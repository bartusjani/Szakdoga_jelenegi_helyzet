using UnityEngine;

public class MovementHuman : MonoBehaviour
{
    Transform player;

    public float speed = 2f;
    public float stopDis = 3f;

    private Vector2 moveDir;
    private bool facingRight = true;

    private Rigidbody2D rb;

    EnemyHealth health;
    HumanAttacks ha;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ha = GetComponent<HumanAttacks>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        FlipTowardsPlayer();
        if (player != null)
        {
            TargetingPlayer();

        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.y == 0) rb.linearVelocity = moveDir * speed;
    }

    void TargetingPlayer()
    {
        if (player == null) return;
        animator.SetBool("Walk", true);
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (ha.PlayerInDashArea()&& ha.canForwardAttack)
        {
            animator.SetBool("Walk", false);
            moveDir = Vector2.zero;
            ha.PreformForwardAttack();
        }
        if (distToPlayer > 2f)
        {
            moveDir = (player.position - transform.position).normalized;
        }
        else
        {
            animator.SetBool("Walk", false);
            moveDir = Vector2.zero;
            ha.ChooseAttack();
        }
    }

    void FlipTowardsPlayer()
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

    void Idle()
    {

    }

}


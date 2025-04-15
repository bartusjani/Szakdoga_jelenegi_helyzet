using System.Collections;
using UnityEngine;

public class HumanAttacks : MonoBehaviour
{
    public LayerMask playerLayer;

    public Transform attackPoint;
    public Transform forwardAttackPoint;

    Animator animator;
    Collider2D humanCollider;
    Rigidbody2D rb;

    public float attackRange = 1.5f;
    public float blockChance = 0.5f;

    public int quickDamage = 15;
    public float quickCooldown = 0.5f;

    public int strongDamage = 25;
    public float strongCooldown = 1f;

    public int forwardDamage = 15;
    public float forwardCooldown = 1.5f;
    public bool canForwardAttack = false;
    bool forwardAttack = false;

    public bool isBlocking = false;
    public float blockCooldown = 2f;

    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        humanCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer) != null;
    }

    Vector2 PlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.transform.position : Vector2.zero;
    }
    public void ChooseAttack()
    {
        if (Time.time < nextAttackTime) return;

        int attackRoll = Random.Range(0, 100);
        if (Random.Range(0f, 1f) <= blockChance)
        {
            StartCoroutine(PreformBlock());
            nextAttackTime = Time.time + blockCooldown;
        }
        else if (attackRoll < 30)
        {
            Debug.Log("tudna forwardolni");
            canForwardAttack = true;
        }
        else if (attackRoll < 50)
        {
            StartCoroutine(QuickSlash());
            nextAttackTime = Time.time + quickCooldown;
        }
        else
        {
            StartCoroutine(StrongSlash());
            nextAttackTime = Time.time + strongCooldown;
        }
    }
    public void PreformForwardAttack()
    {
        StartCoroutine(ForwardSlash());
        nextAttackTime = Time.time + forwardCooldown;
    }

    IEnumerator QuickSlash()
    {
        Debug.Log("QuickAttack");

        animator.SetBool("Quick", true);
        yield return new WaitForSeconds(1f);
        DealDamage(attackPoint, quickDamage);
        animator.SetBool("Quick", false);

    }
    IEnumerator StrongSlash()
    {
        Debug.Log("strongAttack");

        animator.SetBool("Strong", true);
        yield return new WaitForSeconds(1f);
        DealDamage(attackPoint, strongDamage);
        animator.SetBool("Strong", false);
        yield return new WaitForSeconds(1f);

    }

    IEnumerator ForwardSlash()
    {
        if (forwardAttack) yield break;
        forwardAttack = true;
        Debug.Log("forwardAttack");

        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();
        animator.SetBool("Dash", true);

        Physics2D.IgnoreCollision(humanCollider, playerCollider, true);

        float attackMoveSpeed = 10f;
        float attackDuration = 0.3f;
        float elapsedTime = 0f;

        Vector2 startPosition = rb.position;

        int direction = transform.localScale.x > 0 ? 1 : -1;
        Vector2 targetPosition = (Vector2)transform.position + new Vector2(5f * direction, 0);


        while (elapsedTime < attackDuration)
        {

            rb.linearVelocity = (targetPosition - startPosition).normalized * attackMoveSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        transform.position = (Vector3)targetPosition + new Vector3(1f, 0f, 0f); ;

        yield return new WaitForSeconds(1f);
        DealDamage(attackPoint, forwardDamage);

        animator.SetBool("Dash", false);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreCollision(humanCollider, playerCollider, false);
        canForwardAttack = false;
        forwardAttack = false;
    }
    IEnumerator PreformBlock()
    {
        isBlocking = true;
        Debug.Log("block");
        animator.SetBool("Block", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Block", false);
        isBlocking = false;
    }

    void DealDamage(Transform attackPoint, int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
    public bool PlayerInDashArea()
    {

        Collider2D player = Physics2D.OverlapCircle(forwardAttackPoint.position, attackRange, playerLayer);
        return player != null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(forwardAttackPoint.position, attackRange);
    }
}

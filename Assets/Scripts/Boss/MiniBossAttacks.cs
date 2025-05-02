
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiniBossAttacks : MonoBehaviour
{
    EnemyHealth eh;
    public Transform attackPoint;
    public Transform areaAttackPoint;
    public Transform slashAttackPoint;
    public LayerMask playerLayer;
    public Transform player;
    Rigidbody2D rb;
    Animator animator;
    Collider2D bossCollider;

    float blockChance = 0.2f;
    public bool isBlocking = false;


    float strongCooldown = 2f;
    float areaCooldown = 2.5f;
    float blockCooldown = 1f;

    public float attackRange = 2f;
    public float areaAttackRange = 5f;

    int quickDamage = 10;
    int strongDamage = 40;
    int areaDamage = 20;
    int slashDamage = 50;

    float nextAttackTime = 0f;


    public bool phase2 = false;

    //bool attack = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        eh=GetComponent<EnemyHealth>();
        bossCollider = GetComponent<Collider2D>();
    }


    public void ChoosePhase()
    {

        if(eh.Health < 150)
        {
            phase2 = true;
            Phase2();
        }
        else
        {
            Phase1();
        }
    }


    void Phase1()
    {
        Debug.Log("nextattackTime" + nextAttackTime + "Time.time" + Time.time);
        if (Time.time >= nextAttackTime)
        {

            int attackRoll = Random.Range(0, 100);


            if (Random.Range(0f, 1f) <= blockChance)
            {
                StartCoroutine(PreformBlock());
                nextAttackTime =Time.time + blockCooldown;
            }
            else
            {
                if (attackRoll < 70)
                {
                    StartCoroutine(PreformStrongSlash());
                    nextAttackTime =Time.time + strongCooldown;
                }
                else
                {
                    StartCoroutine(PreformAreaAttack());
                    nextAttackTime =Time.time + areaCooldown;
                }
            }
        }
        else return;



    }
    void Phase2()
    {
        //Debug.Log("nextattackTime" + nextAttackTime+"Time.time"+Time.time);
        if (Time.time >= nextAttackTime)
        {
            
            int attackRoll = Random.Range(0, 100);
            //Debug.Log("attackroll"+attackRoll);

            if (Random.Range(0f, 1f) <= blockChance)
            {
                StartCoroutine(PreformBlock());
                nextAttackTime =blockCooldown;
            }
            else
            {
                if (attackRoll < 35)
                {
                    StartCoroutine(PreformForwardSlash());
                    nextAttackTime =Time.time + 5f;
                }
                else
                {
                    StartCoroutine(PreformQuickSlash());
                    nextAttackTime =Time.time + 1f;
                }
            }
        }
        else return;
    }

    IEnumerator PreformStrongSlash()
    {

        Debug.Log("strongSlash");

        animator.SetBool("StrongAttack", true);
        yield return new WaitForSeconds(1.2f);
        DealDamage(attackPoint, strongDamage);

        animator.SetBool("StrongAttack", false);


    }

    IEnumerator PreformBlock()
    {
        isBlocking = true;
        animator.SetBool("BlockStart", true);
        Debug.Log("block");

        yield return new WaitForSeconds(1f);
        animator.SetBool("BlockStart", false);
        isBlocking = false;
    }
    IEnumerator PreformAreaAttack()
    {
        Debug.Log("AreaAttack");

        animator.SetBool("AreaAttack", true);
        yield return new WaitForSeconds(0.8f);
        DealDamage(areaAttackPoint, areaDamage,areaAttackRange);
        animator.SetBool("AreaAttack", false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PreformForwardSlash()
    {

        Debug.Log("forward s");
        GetComponent<MiniBossMovement>().isDashing = true;
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();

        animator.SetBool("DashAttack", true);
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);

        float attackMoveSpeed = 15f;


        Vector2 startPosition = rb.position;


        Vector2 targetPosition;
        if (transform.position.x<player.position.x)
        {
            targetPosition = player.position + new Vector3(10f,0,0);
        }
        else
        {
            targetPosition = player.position - new Vector3(10f, 0, 0);
        }

        
        float distance = Vector2.Distance(startPosition, targetPosition);
        float attackTime = distance / attackMoveSpeed;
        float elapsedTime = 0f;
        bool wasRanThroughPlayer=false;
        while (elapsedTime < attackTime)
        {
            if(Vector2.Distance(rb.position, player.position) < 2f && !wasRanThroughPlayer)
            {
                wasRanThroughPlayer = true;
                DealDamage(slashDamage);
            }
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / attackTime));
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        rb.linearVelocity = Vector2.zero;

        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);

        yield return new WaitForSeconds(1f);
        GetComponent<MiniBossMovement>().isDashing = false;
        animator.SetBool("DashAttack", false);
    }

    IEnumerator PreformQuickSlash()
    {
        //if (attack) yield break;
        //attack = true;
        Debug.Log("quick slash from boss");
        animator.SetBool("QuickAttack", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("QuickAttack", false);

        DealDamage(attackPoint, quickDamage);
        yield return new WaitForSeconds(0.5f);
        //attack = false;
    }

    void DealDamage(int damage)
    {
        player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }

    void DealDamage(Transform attackPoint,int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
    void DealDamage(Transform attackPoint, int damage,float range)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, range, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    public bool PlayerInDashArea()
    {
        
        Collider2D player = Physics2D.OverlapCircle(slashAttackPoint.position,attackRange, playerLayer);
        return player != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
        Gizmos.DrawWireSphere(slashAttackPoint.position, attackRange);
    }

}

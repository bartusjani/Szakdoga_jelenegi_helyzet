using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public Transform jumpAttackPoint;
    public Transform areaAttackPoint;
    public Movement movement;
    public GameObject shield;
    Animator animator;

    [SerializeField] private AudioClip[] attackClips;

    public int damage=10;
    public int strongDamage = 15;
    public int areaDamage = 10;


    public bool isBlocking = false;
    public float blockCooldown = 3f;
    private bool canBlock = true;

    public int comboStep = 0;
    float comboTime = 1.5f;
    float LastAttackTime = 0f;

    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    float areaAttackRange = 1.5f;
    float attackTime = 0f;
    public int attackRate = 2;
    bool isAttacking = false;

    private float clickStartTime;
    private float holdThreshold=0.5f;
    private bool isHolding;
    private bool strongAttackStarted;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (Time.time >= attackTime && !isAttacking)
        {
            if(Input.GetKeyDown(KeyCode.Tab) && canBlock &&movement.IsGrounded())
            {
                StartCoroutine(Block());
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !movement.IsGrounded())
            {
                StartCoroutine(JumpAttack());
                attackTime = Time.time + 0.2f / attackRate;
            }
            if (Input.GetMouseButtonDown(0) && movement.IsGrounded())
            {
                clickStartTime = Time.time;
                isHolding = true;
                strongAttackStarted = false;
            }

            if (isHolding && !strongAttackStarted && Input.GetMouseButton(0))
            {
                float heldTime = Time.time - clickStartTime;

                if (heldTime >= holdThreshold)
                {
                    StartCoroutine(StrongAttack());
                    attackTime = Time.time + 0.5f / attackRate;
                    strongAttackStarted = true;
                }
            }

            if (Input.GetMouseButtonUp(0) && isHolding)
            {
                if (!strongAttackStarted)
                {
                    HandleCombo();
                }

                isHolding = false;
                strongAttackStarted = false;
            }
            //else if (Input.GetKeyDown(KeyCode.Mouse0) && movement.IsGrounded())
            //{
            //    HandleCombo();
            //}
            //else if (Input.GetMouseButtonDown(0) && movement.IsGrounded())
            //{
            //    StartCoroutine(StrongAttack());
            //    attackTime = Time.time + 3f / attackRate;
            //}
            else if (Input.GetKeyDown(KeyCode.Mouse1) && movement.IsGrounded())
            {
                StartCoroutine(AreaAttack());
                attackTime = Time.time + 0.2f / attackRate;
                
            }

        }
    }


    void HandleCombo()
    {
        float timeFromLastAttack = Time.time - LastAttackTime;

        if(timeFromLastAttack > comboTime)
        {
            comboStep = 0;
        }

        comboStep++;

        switch (comboStep)
        {
            case 1:
                StartCoroutine(QuickAttack());
                break;

            case 2:
                StartCoroutine(QuickAttackCombo());
                break;

            case 3:
                comboStep = 1;
                break;
        }

        LastAttackTime = Time.time;
        attackTime = Time.time + 0.2f / attackRate;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(jumpAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
    }


    IEnumerator Block()
    {
        canBlock = false;
        isBlocking = true;
        shield.SetActive(true);

        animator.SetBool("isBlocking", isBlocking);
        //yield return new WaitForSeconds(0.3f);
        //AudioManager.instance.PlaySound(attackClips[0], transform, 1f);
        yield return new WaitForSeconds(1.1f);

        shield.SetActive(false);
        isBlocking = false;
        animator.SetBool("isBlocking", isBlocking);

        yield return new WaitForSeconds(blockCooldown);

        canBlock = true;
    }

    IEnumerator QuickAttack()
    {
        isAttacking = true;
        Debug.Log("quick attack");

        animator.SetTrigger("isAttacking");
        //yield return new WaitForSeconds(0.3f);
        //AudioManager.instance.PlaySound(attackClips[1], transform, 1f);
        yield return new WaitForSeconds(0.8f);

        DealDamage(attackPoint, attackRange, damage);

        yield return new WaitForSeconds(0.1f);
        isAttacking = false;
    }

    IEnumerator StrongAttack()
    {
        isAttacking = true;
        Debug.Log("strong attack");

        animator.SetTrigger("isStrongAt");
        //yield return new WaitForSeconds(0.3f);
        //AudioManager.instance.PlaySound(attackClips[3], transform, 1f);
        yield return new WaitForSeconds(0.8f);

        DealDamage(attackPoint, attackRange, strongDamage);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    IEnumerator QuickAttackCombo()
    {
        isAttacking = true;
        Debug.Log("quick combo attack");

        animator.SetTrigger("isComboAt");
        //yield return new WaitForSeconds(0.3f);
        //AudioManager.instance.PlaySound(attackClips[2], transform, 1f);
        yield return new WaitForSeconds(1f);

        DealDamage(attackPoint, attackRange, damage + damage);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    IEnumerator JumpAttack()
    {
        isAttacking = true;
        Debug.Log("jump attack");

        animator.SetTrigger("isAttacking");
        //AudioManager.instance.PlaySound(attackClips[1], transform, 1f);
        yield return new WaitForSeconds(1f);
        DealDamage(jumpAttackPoint, attackRange, damage);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    IEnumerator AreaAttack()
    {
        isAttacking = true;
        animator.SetTrigger("isAreaAttack");
        Debug.Log("area attack");

        float attackDur =1.01f;

        //AudioManager.instance.PlaySound(attackClips[4], transform, 1f);
        yield return new WaitForSeconds(attackDur);

        DealDamage(areaAttackPoint, areaAttackRange, areaDamage);

        yield return new WaitForSeconds(0.5f);
        isAttacking = false;


    }

    public void PlayBlockSound()
    {
        AudioManager.instance.PlaySound(attackClips[0], transform, 1f);
    }
    public void PlayQuickSound()
    {
        AudioClip[] quicks = { attackClips[1], attackClips[2] };
        AudioManager.instance.PlayRandomSound(quicks, transform, 1f);
    }
    public void PlayStrongSound()
    {
        AudioManager.instance.PlaySound(attackClips[3], transform, 1f);
    }
    public void PlayAreaSound()
    {
        AudioManager.instance.PlaySound(attackClips[4], transform, 1f);
    }


    public void DealDamage(Transform attackPoint, float range, int damage)
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBlocking && collision.CompareTag("Bullet"))
        {
            
            StartCoroutine(DelayedDestroy(collision));
        }
    }

    IEnumerator DelayedDestroy(Collider2D collision)
    {
        yield return new WaitForSeconds(0.2f);

        if (collision != null && collision.gameObject != null)
        {
            Destroy(collision.gameObject);
        }
    }

}

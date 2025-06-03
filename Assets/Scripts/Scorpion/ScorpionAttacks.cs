using System.Collections;
using UnityEngine;

public class ScorpionAttacks : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask playerLayer;

    Animator animator;

    public float attackRange = 1f;

    public int quickSlashDamage = 10;
    public float quickAttackCooldown = 0.5f;

    public int StrongSlashDamage = 20;
    public float strongSlashCooldown = 1f;

    public int forwardSlashDamage = 10;
    public float forwardSlashCooldown = 1.5f;

    public bool isBlocking = false;
    public float blockCooldown=2f;

    private float nextAttack = 0f;

    [SerializeField] private AudioClip[] attackClips;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextAttack && PlayerInRange())
        {
            ChooseAttack();
        }
    }


    bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer) != null;
    }

    void ChooseAttack()
    {

        int random = Random.Range(0, 100);
        nextAttack = Time.time + quickAttackCooldown;
        if (random < 30)
        {
            StartCoroutine(Block());
            nextAttack = Time.time + blockCooldown;
        }
        if (random < 80)
        {
            StartCoroutine(QuickSlash());
            nextAttack = Time.time + quickAttackCooldown;
        }
        else
        {
            StartCoroutine(StrongSlash());
            nextAttack = Time.time + strongSlashCooldown;
        }
    }

    IEnumerator QuickSlash()
    {
        if (isBlocking) yield break;
        Debug.Log("quick");
        animator.SetBool("isQuickAttack", true);

        yield return new WaitForSeconds(0.8f);
        animator.SetBool("isQuickAttack", false);
        nextAttack = Time.time + quickAttackCooldown;
        DealDamage(quickSlashDamage);
    }
   
    IEnumerator StrongSlash()
    {
        if (isBlocking) yield break;
        animator.SetBool("isStrongAttack",true);
        Debug.Log("strong");
        yield return new WaitForSeconds(1f);
        animator.SetBool("isStrongAttack", false);
        nextAttack = Time.time + strongSlashCooldown;
        DealDamage(StrongSlashDamage);
    }

    IEnumerator Block()
    {
        isBlocking = true;

        animator.SetBool("isBlocking", isBlocking);

        yield return new WaitForSeconds(1f);
        isBlocking = false;
        animator.SetBool("isBlocking", isBlocking);
        yield return new WaitForSeconds(blockCooldown);
    }

    public void PlayQuickSound()
    {
        AudioManager.instance.PlaySound(attackClips[0], transform, 1f);
    }
    public void PlayStrongSound()
    {
        AudioManager.instance.PlaySound(attackClips[2], transform, 1f);
    }
    public void PlayBlockSound()
    {
        AudioManager.instance.PlaySound(attackClips[1], transform, 1f);
    }

    void DealDamage(int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player!= null )
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}

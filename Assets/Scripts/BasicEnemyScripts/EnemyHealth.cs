using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 20;
    int bossPhase2health = 150;
    private int health;
    public EnemyHpBar hpBar;
    public GameObject bossHpBar;
    public GameObject bossTrigger;
    public GameObject libraryTeleport;
    public static event Action OnAnyEnemyDeath;

    public bool isBoss = false;
    public static bool isBossDead = false;
    public bool isStaticDead = false;
    public bool isScorpion = false;
    public bool isHuman = false;
    public bool isStaticEnemy = false;

    public WayPointUI wp;
    public Transform bossRoomTarget;
    public GameObject bossHpBarPhase2;
    public EnemyHpBar bossPhase2;
    Animator animator;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        ScorpionAttacks sa = GetComponent<ScorpionAttacks>();
        MiniBossAttacks mba = GetComponent<MiniBossAttacks>();
        if (isBoss && mba.isBlocking)
        {
            Debug.Log("Blocked attack");
            return;

        }
        else if (isScorpion && sa.isBlocking)
        {
            Debug.Log("Blocked attack");
            return;
        }
        else if (isHuman)
        {
            HumanAttacks ha = GetComponent<HumanAttacks>();
            if (ha.isBlocking)
            {
                Debug.Log("Blocked attack");
                return;
            }
        }
        if (isStaticEnemy)
        {
            health -= damage;
            hpBar.setHealth(health);
            if (health <= 0)
            {
                Die(1.2f);
                isStaticDead = true;
                wp.SetTarget(bossRoomTarget);
            }
        }
        health -= damage;


        if (isBoss && health <= 150 && bossHpBar.activeSelf)
        {
            bossHpBar.SetActive(false);
            bossHpBarPhase2.SetActive(true);
            hpBar = bossPhase2;
            hpBar.SetMaxHealth(bossPhase2health);
        }


        hpBar.setHealth(health);
        if (health <= 0)
        {
            Die(1f);
        }

    }

    public void Die(float time)
    {
        
        OnAnyEnemyDeath?.Invoke();

        if (isBoss)
        {
            bossTrigger.SetActive(true);
            libraryTeleport.SetActive(true);
            bossHpBarPhase2.SetActive(false);
            animator.SetTrigger("isDead");
            isBossDead = true;
        }
        if (time> 0)
        {
            if (isScorpion)
            {
                animator.SetTrigger("isDead");
            }
            if (isStaticEnemy)
            {
                animator.SetTrigger("isDead");
            }
            if (isHuman)
            {
                animator.SetTrigger("isDead");
            }
            Destroy(gameObject, time);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}

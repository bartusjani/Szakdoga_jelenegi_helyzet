using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 20;
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

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    void Start()
    {
        health = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isBoss)
        {
            MiniBossAttacks mba = GetComponent<MiniBossAttacks>();
            if (mba.isBlocking)
            {
                Debug.Log("Blocked attack");
                return;
            }
        }
        else if (isScorpion)
        {
            ScorpionAttacks sa = GetComponent<ScorpionAttacks>();
            if (sa.isBlocking)
            {
                Debug.Log("Blocked attack");
                return;
            }
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
        else
        {
            health -= damage;
            hpBar.setHealth(health);
            if (health <= 0)
            {
                Die(0f);
            }
        }

    }

    public void Die(float time)
    {
        
        OnAnyEnemyDeath?.Invoke();

        if (isBoss)
        {
            bossTrigger.SetActive(true);
            libraryTeleport.SetActive(true);
            bossHpBar.SetActive(false);
            isBossDead = true;
        }
        if (time> 0)
        {
            Destroy(gameObject, time);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}

using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public HealthBar healthbar;
    PlayerAttack pa;
    public bool isBlocking = false;

    PlayerRespawn pr;
    bool died;

    void Start()
    {
        died = false;
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        pr = GameObject.Find("Player").GetComponent<PlayerRespawn>();
        pa = GetComponent<PlayerAttack>();
    }


    public void TakeDamage(int damage)
    {
        if (pa.isBlocking)
        {
            Debug.Log("Blocked attack");
            return;
        }
        health -= damage;
        healthbar.setHealth(health);
        if (health <= 0) Die();
    }
    public void Die()
    {
        
        pr.Respawn();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

    }
}

using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public GameObject door;
    public int maxHealth = 20;
    private int health;
    public delegate void EnemyKilledHandler();
    public static event EnemyKilledHandler OnEnemyKilled;
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnEnemyKilled.Invoke();
            Destroy(gameObject);
        }
    }
}

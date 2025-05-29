using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public HealthBar healthbar;
    PlayerAttack pa;
    public bool isBlocking = false;

    PlayerRespawn pr;
    bool died;

    public GameObject respawnScreen;
    public Button respawnButton;

    void Start()
    {
        died = false;
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        pr = GameObject.Find("Player").GetComponent<PlayerRespawn>();
        pa = GetComponent<PlayerAttack>();

        respawnScreen.SetActive(false);
        respawnButton.onClick.AddListener(OnRespawnButtonClicked);
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

        died = true;
        respawnScreen.SetActive(true);
        Time.timeScale = 0f;

    }
    public int GetHealth()
    {
        return health;
    }


    public void OnRespawnButtonClicked()
    {
        died = false;
        respawnScreen.SetActive(false);
        Time.timeScale = 1f;            
        pr.Respawn();                   
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
}

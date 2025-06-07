using System.Collections;
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
    [SerializeField]private bool died;

    public GameObject respawnScreen;
    public Button respawnButton;
    Animator animator;

    [SerializeField] private AudioClip deathClip;


    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (health <= 0) StartCoroutine(Die());
    }
    
    public IEnumerator Die()
    {

        died = true;
        animator.SetTrigger("isDead");
        AudioManager.instance.PlaySound(deathClip, transform, 1f);
        yield return new WaitForSeconds(0.8f);
        respawnScreen.SetActive(true);
        Time.timeScale = 0f;

    }
    public bool Died()
    {
        return died;
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

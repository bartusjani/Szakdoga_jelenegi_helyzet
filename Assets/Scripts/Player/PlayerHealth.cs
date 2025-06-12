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
    bool isFirstDeath = false;
    public ItemAdder adder;

    PlayerRespawn pr;
    [SerializeField]private bool died;

    public GameObject respawnScreen;
    public Button respawnButton;
    Animator animator;

    [SerializeField] private AudioClip deathClip;

    public InventoryPage inv;
    public Sprite itemImg;
    string itemName= "Respawn";
    string itemDesc = "This is against the law of the gods. I should be dead. My journey changed me, I already knew... But even now it furiates me. No resting place for us damned. I shouldn't exist yet the mere concept of existing keeps us alive. The Entity.That's the name we call it now, by seeing it it lached onto us. Like a parasite. It has motive now, the thing that stayed with us. That's why we came back... The Goldens...now we are just the doom that's coming. Until we do our task we live on. This is no life, just a purpose. I hope it brings us peace once we are through with it. My mind is jumbled, are the others like me? Full of hate? Empty?\r\n";




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
        if (!isFirstDeath)
        {
            adder.AddItemToInv(inv,itemImg,itemName,itemDesc);
            isFirstDeath = true;
        }
        
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

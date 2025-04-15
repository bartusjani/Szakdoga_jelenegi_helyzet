using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    float damageRate = 0.5f;
    bool isPlayerInContact = false;
    public PlayerHealth playerHealth;
    private Coroutine damageCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth!=null && damageCoroutine==null)
            {
                isPlayerInContact = true;
                damageCoroutine = StartCoroutine(GiveDamage(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInContact = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }

        }
    }

    public IEnumerator GiveDamage(PlayerHealth health)
    {
        while (isPlayerInContact)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageRate);
        }
        damageCoroutine = null;
    }
}

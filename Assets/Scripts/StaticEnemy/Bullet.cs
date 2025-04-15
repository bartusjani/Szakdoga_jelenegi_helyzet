using System.Collections;
using System.ComponentModel;
using System.Net;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 10;

    private Vector2 dir;
    public PlayerHealth ph;
    Animator animator;
    public static int blockedBullets = 0;

    private void Start()
    {

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on bullet prefab!");
        }
    }
    public void SetDir(Vector2 direction)
    {
        dir = direction.normalized;
    }
    void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.gameObject.tag)
        {
            case "Player":
                animator.SetTrigger("isCollided");

                ph = collision.gameObject.GetComponent<PlayerHealth>();
                ph.TakeDamage(damage);
                StartCoroutine(DestroyWithDelay());
                
                break;
            case "Shield":
                animator.SetTrigger("isCollided");
                blockedBullets++;
                Debug.Log("Blocked  bullets:" + blockedBullets);
                StartCoroutine(DestroyWithDelay());
                
                break;
            case "Wall":
                animator.SetTrigger("isCollided");

                StartCoroutine(DestroyWithDelay());
                
                break;
        }
    }


    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

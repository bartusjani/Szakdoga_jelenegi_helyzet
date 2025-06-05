using UnityEngine;

public class StaticTrigger : MonoBehaviour
{
    public GameObject statEnemy;
    Animator animator;

    private void Start()
    {
        if (statEnemy != null)
        {
            animator = statEnemy.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            statEnemy.SetActive(true);
            if(animator != null)
            {
                animator.Play("static_enemy_spawn");
            }
        }
    }
}

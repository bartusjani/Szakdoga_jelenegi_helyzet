using UnityEngine;



public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    private float horizontal;
    private bool isFacingRight=true;

    private float distance;
    public float distanceBetween;



    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

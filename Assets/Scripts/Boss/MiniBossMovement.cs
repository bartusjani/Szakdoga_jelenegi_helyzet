using System.Collections;
using UnityEngine;

public class MiniBossMovement : MonoBehaviour
{
    public Transform player;

    public float speed = 3f;
    public float stopDis = 1f;
    public bool isDashing = false;
    private Vector2 moveDir;
    private bool facingRight = true;

    private Rigidbody2D rb;
    Animator animator;
    EnemyHealth health;
    MiniBossAttacks mba;

    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private AudioClip[] soundClips;
    private float footstepTimer=0f;
    private float footstepInterval=0.5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mba = GetComponent<MiniBossAttacks>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void Update()
    {
        
        FlipTowardsPlayer();
        if (player != null)
        {

            StartCoroutine(TargetingPlayer());

        }

        if (Mathf.Abs(moveDir.x) > 0.1f && !isDashing)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                PlayWalkSound();
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }
    private void FixedUpdate()
    {
        if (rb.linearVelocity.y == 0) rb.linearVelocity = moveDir * speed;
    }

    public IEnumerator TargetingPlayer()
    {
        animator.SetTrigger("GettingUp");
        yield return new WaitForSeconds(1f);
        if (player == null) yield break ;
        float distToPlayer = Vector2.Distance(transform.position, player.position)-4f;

        if (mba.PlayerInDashArea() && mba.phase2)
        {
            animator.SetBool("Walk", false);
            moveDir = Vector2.zero;
            mba.ChoosePhase();
        }
        else if(distToPlayer > stopDis)
        {
            
            animator.SetBool("Walk",true);
            moveDir= (player.position -transform.position).normalized;
        }
        else
        {
            animator.SetBool("Walk", false);
            moveDir = Vector2.zero;
            mba.ChoosePhase();
        }
    }
    public void FlipTowardsPlayer()
    {
        if (!isDashing) 
        { 
            if(player.position.x<transform.position.x && facingRight)
            {
                Flip();
            }
            else if(player.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }

        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newS = transform.localScale;
        newS.x *= -1;
        transform.localScale = newS;
    }

    public void PlayWalkSound()
    {
        AudioManager.instance.PlayRandomSound(walkClips, transform, 1f);
    }
    public void PlayBlockGoingInSounds()
    {
        AudioManager.instance.PlaySound(soundClips[0], transform, 1f);
    }
    public void PlayBlockGoingOutSounds()
    {
        AudioManager.instance.PlaySound(soundClips[1], transform, 1f);
    }

}

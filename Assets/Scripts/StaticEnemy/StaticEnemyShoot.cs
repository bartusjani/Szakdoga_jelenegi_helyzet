using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;

    Animator fireballAnimator;

    private float fireRate = 2f;
    private float fireTimer;

    private bool canShoot = true;
    private Transform player;

    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip[] sounds;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

    }

    private void Update()
    {
        if (player == null|| !canShoot) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0;
        }

        if (Bullet.blockedBullets >= 3)
        {
            
            StartCoroutine(ShootingPaused());
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {

            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);

            fireballAnimator = bullet.GetComponent<Animator>();

            fireballAnimator.Play("shooting_fireball");

            Vector2 dir = (player.position - firepoint.position).normalized;
            bullet.GetComponent<Bullet>().SetDir(dir);
        }
    }

    private IEnumerator ShootingPaused()
    {
        canShoot = false;
        Bullet.blockedBullets = 0;
        AudioManager.instance.PlayRandomSound(sounds, transform, 1f);
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }

    public void PlaySpitSound()
    {
        AudioManager.instance.PlaySound(attackClip, transform, 1f);
    }
}

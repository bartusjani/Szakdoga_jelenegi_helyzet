using UnityEngine;

public class FightMusic : MonoBehaviour
{
    //[SerializeField] AudioClip music;
    [SerializeField] GameObject enemyMusic;
    [SerializeField] GameObject backgroundMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        backgroundMusic.SetActive(false);
        enemyMusic.SetActive(true);

        //AudioManager.instance.PlaySound(music,transform,1f);

    }
}

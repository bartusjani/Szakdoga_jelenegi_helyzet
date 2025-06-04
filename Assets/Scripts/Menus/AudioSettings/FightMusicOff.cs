using UnityEngine;

public class FightMusicOff : MonoBehaviour
{
    [SerializeField] GameObject enemyMusic;
    [SerializeField] GameObject backgroundMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyMusic.SetActive(false);
        backgroundMusic.SetActive(true);

    }
}

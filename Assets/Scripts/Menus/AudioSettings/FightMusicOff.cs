using UnityEngine;

public class FightMusicOff : MonoBehaviour
{
    [SerializeField] GameObject enemyMusic;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject secondBackgroundMusic;

    private bool wasBossDead = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyMusic.SetActive(false);
        backgroundMusic.SetActive(true);
    }

    private void Update()
    {
        if (EnemyHealth.isBossDead && !wasBossDead)
        {
            enemyMusic.SetActive(false);
            secondBackgroundMusic.SetActive(true);
            wasBossDead = true;
        }
    }
}

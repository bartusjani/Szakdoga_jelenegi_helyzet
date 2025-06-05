using UnityEngine;

public class FightMusic : MonoBehaviour
{
    [SerializeField] GameObject enemyMusic;
    [SerializeField] GameObject backgroundMusic;
    private bool hasTriggered=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        hasTriggered = true;
        backgroundMusic.SetActive(false);
        enemyMusic.SetActive(true);
    }

}

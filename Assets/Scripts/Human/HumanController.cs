using System.Collections;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    private int totalHumans;
    private int deadHumans = 0;
    public bool allDead = false;

    [SerializeField] GameObject enemyMusic;
    [SerializeField] GameObject secondMusic;


    [SerializeField] Sprite itemIcon;
    [SerializeField] string title;
    [SerializeField] string desc;
    [SerializeField] InventoryPage inv;

    ItemAdder adder;
    private void Start()
    {
        adder = GetComponent<ItemAdder>();
        StartCoroutine(CountHumansAfterDelay());

        Debug.Log(totalHumans);
        EnemyHealth.OnAnyEnemyDeath += OnEnemyDeath;
    }
    private void Update()
    {
        if (totalHumans == 0 && !allDead )
        {
            totalHumans = GameObject.FindGameObjectsWithTag("Human").Length;
            Debug.Log("Total Humans: " + totalHumans);
        }
    }

    private void OnEnemyDeath()
    {
        deadHumans++;

        if (deadHumans >= totalHumans)
        {
            allDead = true;
            adder.AddItemToInv(inv, itemIcon, title, desc);
            enemyMusic.SetActive(false);
            secondMusic.SetActive(true);
            this.enabled = false;

        }
    }

    private IEnumerator CountHumansAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        totalHumans = GameObject.FindGameObjectsWithTag("Human").Length;
        Debug.Log("Total Humans: " + totalHumans);
    }

    private void OnDestroy()
    {
        EnemyHealth.OnAnyEnemyDeath -= OnEnemyDeath;
    }
}

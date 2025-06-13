using UnityEngine;

public class GroundDoorTrigger : MonoBehaviour
{
    public bool groundDoorTrigger=false;
    public Transform groundDoorTarget;
    public WayPointUI wp;
    private int allScorpions;
    private int deadScorpions = 0;
    public GameObject secondDoor;
    public bool allDead=false;


    private void Start()
    {
        allScorpions = GameObject.FindGameObjectsWithTag("Scorpion").Length;
        Debug.Log("Scorpions:" + allScorpions);

        EnemyHealth.OnAnyEnemyDeath += EnemyDeathHandler;

    }

    private void Update()
    {
        if (allScorpions == 0 && !allDead)
        {
            allScorpions = GameObject.FindGameObjectsWithTag("Scorpion").Length;
            //Debug.Log("Scorpions:" + allScorpions);
        }
    }

    private void EnemyDeathHandler()
    {
        deadScorpions++;
        //Debug.Log("Enemy remaining:" + (allScorpions - deadScorpions));

        if(deadScorpions>= allScorpions)
        {
            allDead = true;
            //Debug.Log("All died");
            secondDoor.SetActive(true);
            secondDoor.GetComponent<AllPopupController>().enabled = false;
            groundDoorTrigger = true;
            
        }
    }

    
    private void OnDestroy()
    {
        EnemyHealth.OnAnyEnemyDeath -= EnemyDeathHandler;
    }


}

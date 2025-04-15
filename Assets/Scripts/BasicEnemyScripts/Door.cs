using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the enemy killed event
        DoorOpening.OnEnemyKilled += OpenDoor;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        DoorOpening.OnEnemyKilled -= OpenDoor;
    }

    private void Start()
    {
       
    }

    private void OpenDoor()
    {
        Debug.Log("The door has opened!");

       GetComponent<Collider2D>().enabled = false;
    }
}

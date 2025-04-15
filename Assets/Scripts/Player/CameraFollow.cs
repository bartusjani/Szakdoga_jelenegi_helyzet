using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    private Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        position.x = player.transform.position.x;
        position.y = player.transform.position.y;
        transform.position = position;
    }
}

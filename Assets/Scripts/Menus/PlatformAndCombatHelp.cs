using UnityEngine;

public class PlatformAndCombatHelp : MonoBehaviour
{

    bool isTutorialRoom;
    GameObject platformerHelp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTutorialRoom)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                platformerHelp.SetActive(false);
            }
        }
    }
}

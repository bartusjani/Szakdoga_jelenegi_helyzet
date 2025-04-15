using System.Collections;
using UnityEngine;

public class ControllerPopUpBubble : MonoBehaviour
{
    public PopUpBubble prefab;
    public string message;

    private PopUpBubble activeBubble;

    bool isPlayerInTrigger = false;
    private bool wasSpeaking=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            isPlayerInTrigger = true;
            SetPopUp(message);
        }
        else
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;
            }
        }
    }
    void SetPopUp(string message)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(prefab, parent);
            activeBubble.SetText(message);
        }
    }
}

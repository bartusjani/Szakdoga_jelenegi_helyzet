using System.Collections;
using UnityEngine;

public class PlayerTutorialTrigger : MonoBehaviour
{
    public GameObject combatHelp;
    public GameObject blockHelp;
    public GameObject platformerHelp;
    int helpcounter = 0;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformCollider") && helpcounter<1)
        {
            //platform
            platformerHelp.SetActive(true);
            StartCoroutine(SetsActiveFalse(platformerHelp));
        }
        else if (collision.CompareTag("SecondDoor") && helpcounter < 2)
        {
            //platform
            combatHelp.SetActive(true);
            StartCoroutine(SetsActiveFalse(combatHelp));
        }
        else if (collision.CompareTag("BlockCollider") && helpcounter < 3)
        {
            //platform
            blockHelp.SetActive(true);
            StartCoroutine(SetsActiveFalse(blockHelp));
        }
    }

    IEnumerator SetsActiveFalse(GameObject gameObject)
    {
        helpcounter++;
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);

    }
}

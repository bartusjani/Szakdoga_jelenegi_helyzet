using UnityEngine;
using UnityEngine.SceneManagement;

public class Outro : MonoBehaviour
{
    public GameObject interactText;
    bool isPlayerInTrigger = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            SceneManager.LoadScene("Outro");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            interactText.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            interactText.SetActive(false);
        }
    }
}

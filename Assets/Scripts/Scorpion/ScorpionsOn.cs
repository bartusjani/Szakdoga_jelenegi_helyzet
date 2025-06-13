using UnityEngine;

public class ScorpionsOn : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject script;

    private bool hasActivated = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hasActivated)
        {
            hasActivated = true;
            enemy1.SetActive(true);
            enemy2.SetActive(true);
        
            script.SetActive(true);
            this.enabled = false;

        }

    }
}

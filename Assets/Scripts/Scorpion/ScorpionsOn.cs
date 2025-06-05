using UnityEngine;

public class ScorpionsOn : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject script;


    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy1.SetActive(true);
        enemy2.SetActive(true);
        
        script.SetActive(true);
        this.enabled = false;

    }
}

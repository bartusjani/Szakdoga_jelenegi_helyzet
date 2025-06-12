using UnityEngine;

public class InvClose : MonoBehaviour
{

    public GameObject inv;
    public GameObject button;

    public void CloseInventory()
    {
        inv.SetActive(false);
        button.SetActive(false);
        Time.timeScale = 1f;
    }
}

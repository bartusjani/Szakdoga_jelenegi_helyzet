using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    public InventoryPage invUI;

    public int invSize = 15;
    private void Start()
    {
        invUI.IntializeInv(invSize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (invUI.isActiveAndEnabled == false)
            {
                invUI.Show();
            }
            else
            {
                invUI.Hide();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && invUI.isActiveAndEnabled)
        {
            invUI.Hide();
        }
    }
    public void OpenFromPause()
    {
        invUI.Show(true);
    }
}

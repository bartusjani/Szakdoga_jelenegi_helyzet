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
    }
}

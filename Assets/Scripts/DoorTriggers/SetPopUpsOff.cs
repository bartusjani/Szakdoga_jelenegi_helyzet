using Unity.VisualScripting;
using UnityEngine;

public class SetPopUpsOff : MonoBehaviour
{
    public Trigger tr;
    private void Update()
    {
        if (tr.isDoorOpen)
        {
            gameObject.GetComponent<AllPopupController>().enabled = false;
        }
    }
}

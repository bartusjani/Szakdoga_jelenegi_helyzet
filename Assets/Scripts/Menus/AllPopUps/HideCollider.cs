using UnityEngine;

public class HideCollider : MonoBehaviour
{
    public GameObject doorWithSecondPopUpController;

    private void Update()
    {
        if (doorWithSecondPopUpController != null) doorWithSecondPopUpController.SetActive(false);
    }
}

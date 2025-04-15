using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBubble : MonoBehaviour
{
    public TMP_Text PopUpText;

    public void SetText(string text)
    {
        PopUpText.text = text;
    }
}

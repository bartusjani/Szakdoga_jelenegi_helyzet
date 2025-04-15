using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public TMP_Text speechText;

    public void SetText(string text)
    {
        speechText.text = text;
    }
}

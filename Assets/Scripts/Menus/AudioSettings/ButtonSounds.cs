using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;

    public void OnButtonClicked()
    {
        AudioManager.instance.PlaySound(clickSound,transform,1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound(hoverSound, transform, 1f);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider brightnessSlider;
    public Image brightnessOverlay;

    void Start()
    {
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        SetBrightness(brightnessSlider.value);
    }

    public void SetBrightness(float value)
    {
        Color color = brightnessOverlay.color;
        color.a = 1f - value;
        brightnessOverlay.color = color;
    }
}

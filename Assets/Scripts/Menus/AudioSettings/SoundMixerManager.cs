using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider effectVolumeSlider;

    void Start()
    {
        masterVolumeSlider.minValue = 0.0001f;
        masterVolumeSlider.maxValue = 1f;
        masterVolumeSlider.value = 1f;

        musicVolumeSlider.minValue = 0.0001f;
        musicVolumeSlider.maxValue = 1f;
        musicVolumeSlider.value = 1f;

        effectVolumeSlider.minValue = 0.0001f;
        effectVolumeSlider.maxValue = 1f;
        effectVolumeSlider.value = 1f;
    }
    public void SetMasterVolume(float level)
    {

        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20);
    }
    public void SetEffectVolume(float level)
    {
        audioMixer.SetFloat("effectVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);

    }
}

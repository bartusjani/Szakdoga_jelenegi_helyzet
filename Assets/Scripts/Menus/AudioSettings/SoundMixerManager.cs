using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {

        audioMixer.SetFloat("masterVolume", Mathf.Log10(level)*20);
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

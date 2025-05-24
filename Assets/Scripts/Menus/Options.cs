using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsUI;

    public AudioMixer audioMixer;

    PauseMenu pm;
    

    Resolution[] res;
    void Start()
    {
        pm = GetComponent<PauseMenu>();
    //    res = Screen.resolutions;

    //    List<string> options = new List<string>();

    //    int currentResIndex = 0;
    //    for (int i = 0; i < res.Length; i++)
    //    {
    //        string option = res[i].width + " x " + res[i].height;

    //        options.Add(option);
    //        if (res[i].width == Screen.currentResolution.width 
    //            && res[i].height == Screen.currentResolution.height) currentResIndex = i;
    //    }

    }

    //public void SetResolution(int index)
    //{
    //    Resolution resolution = res[index];
    //    Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    //}
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Back()
    {
        pm.isOptionsOpened = false;
        mainMenuUI.SetActive(true);
        optionsUI.SetActive(false);
    }
}

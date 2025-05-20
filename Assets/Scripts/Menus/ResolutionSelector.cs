using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ResolutionSelector : MonoBehaviour
{
    public TextMeshProUGUI resolutionText;
    public Button leftArrowButton;
    public Button rightArrowButton;

    private Resolution[] resolutions;
    private int currentIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;

        List<Resolution> uniqueResolutions = new List<Resolution>();

        foreach (Resolution r in resolutions)
        {
            if (!uniqueResolutions.Exists(x => x.width == r.width && x.height == r.height))
            {
                uniqueResolutions.Add(r);
            }
        }

        resolutions = uniqueResolutions.ToArray();

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
                break;
            }
        }

        UpdateResolutionText();

        leftArrowButton.onClick.AddListener(PreviousResolution);
        rightArrowButton.onClick.AddListener(NextResolution);
    }

    void UpdateResolutionText()
    {
        resolutionText.text = $"{resolutions[currentIndex].width} x {resolutions[currentIndex].height} px";
    }

    void PreviousResolution()
    {
        currentIndex = (currentIndex - 1 + resolutions.Length) % resolutions.Length;
        ApplyResolution();
    }

    void NextResolution()
    {
        currentIndex = (currentIndex + 1) % resolutions.Length;
        ApplyResolution();
    }

    void ApplyResolution()
    {
        Resolution r = resolutions[currentIndex];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        UpdateResolutionText();
    }
}

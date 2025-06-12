using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public VideoPlayer videoplayer;

    public GameObject skipText;
    private bool skipPromptShown = false;

    void Start()
    {
        videoplayer.prepareCompleted += OnVideoPrepared;
        videoplayer.loopPointReached += OnVideoFinished;

        videoplayer.Prepare();
    }

    void Update()
    {
        if (Input.anyKeyDown && !skipPromptShown)
        {
            ShowSkipPrompt();
        }

        if (skipPromptShown && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipVideo();
        }
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        double lengthInSeconds = vp.length;
        Debug.Log("A vide� hossza: " + lengthInSeconds + " m�sodperc.");
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void ShowSkipPrompt()
    {
        skipPromptShown = true;
        if (skipText != null)
        {
            skipText.SetActive(true);
        }
    }

    void SkipVideo()
    {
        Debug.Log("Vide� �tugorva ESC megnyom�s�val.");
        videoplayer.Stop();
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Game");
    }

}

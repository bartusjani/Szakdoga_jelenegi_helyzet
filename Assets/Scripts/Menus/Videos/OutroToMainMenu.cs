using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OutroToMainMenu : MonoBehaviour
{
    public VideoPlayer videoplayer;

    void Start()
    {
        videoplayer.loopPointReached += OnVideoFinished;

        videoplayer.Prepare();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

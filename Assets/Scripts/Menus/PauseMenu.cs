using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public bool isOptionsOpened = false;

    public GameObject pauseMenuUI;
    public GameObject hpUI;
    public GameObject inGameMenu;
    public GameObject player;
    public GameObject settingsMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isOptionsOpened)
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        isOptionsOpened = false;
        inGameMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        hpUI.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    void Pause()
    {
        inGameMenu.SetActive(false);
        hpUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void LoadOptions()
    {
        isOptionsOpened = true;
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void OpenInventoryFromPause()
    {
        player.GetComponent<InventoryController>().OpenFromPause();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseButton, ResumeButton, ResetButton, QuitButton, PauseMenu, pauseImage;
    
    public TextMeshProUGUI text;

    private bool gamePaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                resume();
            } 
            else
            {
                pause();
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        PauseMenu.SetActive(true); 
        PauseButton.SetActive(false);
        text.enabled = false;
    }

    public void resume()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false); 
        text.enabled = true;
    }

    public void restart()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
}

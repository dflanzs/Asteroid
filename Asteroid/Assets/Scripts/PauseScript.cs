using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public GameObject ResetButton;
    public GameObject QuitButton;
    public GameObject PauseMenu;
    public GameObject pauseImage;

    private bool gamePaused = false;

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (gamePaused){
                resume();
            } else {
                pause();
            }
        }
    }

    public void pause(){
        Time.timeScale = 0f;
        gamePaused = true;
        PauseMenu.SetActive(true); 
        PauseButton.SetActive(false);
        ResumeButton.SetActive(true);
        ResetButton.SetActive(true);
        QuitButton.SetActive(true);
        pauseImage.SetActive(true);
    }

    public void resume(){
        Time.timeScale = 1f;
        gamePaused = false;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false); 
        ResumeButton.SetActive(false);
        ResetButton.SetActive(false);
        QuitButton.SetActive(false);
        pauseImage.SetActive(false);
    }

    public void restart(){
        gamePaused = false;
        PauseButton.SetActive(true);
        PauseMenu.SetActive(false); 
        ResumeButton.SetActive(false);
        ResetButton.SetActive(false);
        QuitButton.SetActive(false);
        pauseImage.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit(){
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
}

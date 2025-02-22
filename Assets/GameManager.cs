using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    public bool isDead = false;
    public GameObject pausePanel;
    public GameObject deathPanel;
    public GameObject HUD;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            isPaused = false;
            Continue();
        }else if(Input.GetKeyDown(KeyCode.R) && isPaused)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }else if(Input.GetKeyDown(KeyCode.M) && isPaused)
        {
            MainMenu();
            Time.timeScale = 1;
        }

        if(isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenu();
                Time.timeScale = 1;
            }
        }
    }

    public void Pause()
    {
        GameObject.Find("main_body").GetComponent<shoot>().enabled = false;
        HUD.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        GameObject.Find("main_body").GetComponent<shoot>().enabled = true;
        HUD.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void DeathMenu()
    {
        HUD.SetActive(false);
        deathPanel.SetActive(true);
        isDead = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
    }
}

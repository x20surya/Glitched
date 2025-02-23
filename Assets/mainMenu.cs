using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ExitGame(){
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
    public void StartGame(){
        Debug.Log("Starting Game...");
        SceneManager.LoadScene("Level2");
    }
}

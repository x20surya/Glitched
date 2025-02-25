using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().Continue();
        }
    }
    public void SetVolume(float vol){
    }
}

using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject victoryPanel;
    public GameObject HUD;
    bool won = false;

    // Update is called once per frame
    public void OnVictory(){
        victoryPanel.SetActive(true);
        HUD.SetActive(false);
        won = true;
    }

    void Update(){
        if(won){
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.M)){
                gameManager.GetComponent<GameManager>().MainMenu();
            }
        }
    }
}

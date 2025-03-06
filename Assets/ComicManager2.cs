using UnityEngine;

public class ComicManager2 : MonoBehaviour
{ 
    private int pg;
    public GameObject pg1;
    public GameObject pg2;
    public GameObject pg3;
    public GameObject pg4;
    public GameObject HUD;
    public GameObject slime;
    public GameObject space;
    void Start() {
        pg = 1;
        pg1.SetActive(true);
        pg2.SetActive(false);
        pg3.SetActive(false);  
        pg4.SetActive(false);
        HUD.SetActive(false);
        slime.SetActive(false);
        space.SetActive(true);  
        Time.timeScale = 0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && pg == 1) {
            pg2.SetActive(true);
            pg = 2;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && pg == 2) {
            pg3.SetActive(true);
            pg = 3;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && pg == 3) {
            pg4.SetActive(true);
            pg = 4;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && pg == 4) {
            pg1.SetActive(false);
            pg2.SetActive(false);
            pg3.SetActive(false);
            slime.SetActive(true);
            HUD.SetActive(true);
            space.SetActive(false);
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using Unity.Cinemachine;

public class Unpossess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isPossessed = false;
    public GameObject slime;
    void Start()
    {
        Debug.Log("Enemy Possessed :: " + isPossessed);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPossessed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnPossess();
            }
        }

    }

    void UnPossess()
    {
        // enable slime 
        if(slime == null){
            Debug.Log("Slime not found");
            return;
        }
        slime.SetActive(true);

        // set position of slime to enemy
        Debug.Log("UnPossessing...");
        slime.transform.position = gameObject.transform.position + new Vector3(0, 10, 0);

        // enable move script in slime
        if (GameObject.Find("main_body").GetComponent<MOTION>() != null)
        {
            GameObject.Find("main_body").GetComponent<MOTION>().enabled = true;
        }

        // pose camera to slime
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.Follow = GameObject.Find("main_body").transform;
            vcam.LookAt = GameObject.Find("main_body").transform;
        }

        // destroy enemy
        Destroy(gameObject);
    }
}

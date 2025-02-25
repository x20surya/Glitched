using System.Collections;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public float range = 5f;
    public LayerMask playerLayer;
    public GameObject F;
    public GameObject KeyNotFound;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, range, playerLayer);
        if(hit.Length == 0) F.SetActive(false);
        else HandlePlayer();
    }

    void HandlePlayer(){
        F.SetActive(true);
        if(Input.GetKeyDown(KeyCode.F)){
            if(GameObject.Find("Global State").GetComponent<Abilities>().hasKey)
            {
                audioManager.PlaySFX(audioManager.doorUnlock, 0.5f);

                StartCoroutine(nextLevel());
            }
            else{
                KeyNotFound.SetActive(true);
                Invoke(nameof(HideKeyNotFound), 2f);
            }
        }
    }

    void HideKeyNotFound(){
        KeyNotFound.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("GameManager").GetComponent<GameManager>().nextLevel();
    }
}

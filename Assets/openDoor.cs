using UnityEditor.ShaderGraph;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    // Update is called once per frame
    public float range = 20f;
    public LayerMask playerLayer;
    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, range, playerLayer);
        if(hit.Length > 0 && GameObject.Find("Global State").GetComponent<Abilities>().hasKey){
            
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
}

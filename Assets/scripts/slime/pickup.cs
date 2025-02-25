using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public float pickupRange = 1f;
    public LayerMask pickupLayer;


    // Update is called once per frame
    void Update()
    {
        checkPickup();

    }
    void checkPickup(){
        // Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayer);
        // foreach(var x in hit){
        //     if(x.gameObject.CompareTag("Rock") && !x.gameObject.GetComponent<rochThrow>().enabled)
        //     {
        //         Destroy(x.gameObject);
        //     }
        // }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

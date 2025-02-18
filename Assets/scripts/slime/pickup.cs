using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    private List<GameObject> pickups = new List<GameObject>();
    public float pickupRange = 1f;
    public LayerMask pickupLayer;


    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Debug.Log("Pickup: " + hitCollider.name);
            // if (!pickups.Exists(p => p.name == hitCollider.gameObject.name))
            // {
            //     pickups.Add(hitCollider.gameObject);
            // }
            Destroy(hitCollider.gameObject);
        }
        // Debug.Log("Pickups: " + pickups.Count);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

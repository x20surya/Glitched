using UnityEngine;

public class rochThrow : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask playerLayer;
    public float rockDetecttionRadius = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerateRecoil();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    
        if(GroundLayer == (GroundLayer | (1 << collision.gameObject.layer))){
            // Alert Knights
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, rockDetecttionRadius);

            foreach(Collider2D x in hit)
            {
                if (x.gameObject.GetComponent<KnightAI>())
                {
                    x.gameObject.GetComponent<KnightAI>().isSus = true;
                    x.gameObject.GetComponent<KnightAI>().susPos = (Vector2)transform.position;
                }
            }

            Destroy(gameObject);
        }
    }

    void GenerateRecoil(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.5f, playerLayer);
        foreach(var x in hit){
            if(x.gameObject.CompareTag("Vertex"))
            {
                x.gameObject.GetComponent<recoil>().AddRecoil();
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rockDetecttionRadius);
    }
}

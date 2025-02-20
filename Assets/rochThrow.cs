using UnityEngine;

public class rochThrow : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask playerLayer;

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
}

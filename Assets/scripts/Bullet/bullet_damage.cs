using UnityEngine;

public class bullet_damage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public float damage = 10f;
    public float remainingTime = 2f;
    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if(remainingTime <= 0)
        {
            Destroy(gameObject); 
        }
        GenerateRecoil();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if(hitObject.layer == enemyLayer)
        {
            // deal damage
            Debug.Log("Hit: " + hitObject.name);
        }
        Destroy(gameObject);
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

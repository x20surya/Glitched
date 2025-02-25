using UnityEngine;

public class bullet_damage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public int damage = 30;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitObject = collision.gameObject;


        // deal damage
        if (hitObject.GetComponent<Health>())
        {
            hitObject.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    void GenerateRecoil(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.5f, playerLayer);
        foreach(var x in hit){
            if(x.gameObject.CompareTag("CurrentPlayer"))
            {
                if (x.gameObject.GetComponent<recoil>()){
                    x.gameObject.GetComponent<recoil>().AddRecoil();
                }
            }
        } 
    }
}

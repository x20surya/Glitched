using UnityEngine;

public class flamethrowerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float flamespeed = 10f;
    public int damage = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * flamespeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("CurrentPlayer"))
        {
            if(collision.GetComponent<Health>()) collision.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}

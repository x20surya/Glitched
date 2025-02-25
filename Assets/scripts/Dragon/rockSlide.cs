using UnityEngine;

public class rockSlide : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public int damage = 40;
    public float pursuingSpeed = 20f;
    Vector3 playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("CurrentPlayer").gameObject.transform.position;
        rb.linearVelocity = new Vector2(Mathf.Abs(pursuingSpeed)*Mathf.Sign(playerPos.x - transform.position.x), -pursuingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("CurrentPlayer"))
        {
            if (collision.GetComponent<Health>()) collision.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}

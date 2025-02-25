using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    Rigidbody2D rb;
    public float knifeSpeed = 10f;
    public int knifeDamage = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * knifeSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("CurrentPlayer"))
        {
            if(GameObject.Find("main_body"))
            GameObject.Find("main_body").GetComponent<Health>().TakeDamage(knifeDamage);
        }

        Destroy(gameObject);
    }
}

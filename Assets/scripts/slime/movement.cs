using UnityEngine;
using UnityEngine.InputSystem;

public class MOTION : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-5, rb.linearVelocity.y);
            // transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(5, rb.linearVelocity.y);
            // transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, 50), ForceMode2D.Impulse);
        }
    }
}

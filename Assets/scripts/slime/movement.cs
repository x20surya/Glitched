using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MOTION : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public GameObject globalStates;


    public float speed = 5f;
    public float jumpForce = 50f;
    public float grondRange = 5f;
    private bool canDash = true;
    public float dashForce = 5f;
    public float dashRate = 2f;
    public float dashDuration = 0.5f;
    private float dashTime;
    public float glideFallSpeed = 5f;
    public float moveForce = 10f;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = dashRate;
    }

    // Update is called once per frame
    void Update()
    {
        dashTime -= math.max(-1, Time.deltaTime);
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-moveForce, 0), ForceMode2D.Force);
            if(dashTime < dashRate - dashDuration) rb.linearVelocity = new Vector2(-math.min(speed, math.abs(rb.linearVelocityX)), rb.linearVelocityY);
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
            {
                Debug.Log("Dash left");
                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
                canDash = false;
                dashTime = dashRate;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(moveForce, 0), ForceMode2D.Force);
            if(dashTime < dashRate - dashDuration) rb.linearVelocity = new Vector2(math.min(speed, rb.linearVelocityX), rb.linearVelocityY);
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
            {
                Debug.Log("Dash right");
                rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
                canDash = false;
                dashTime = dashRate;
            }
            
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (CheckGround())
        {
            if (Input.GetKeyDown(KeyCode.Space)) rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            if (globalStates.GetComponent<Abilities>().canGlide)
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, glideFallSpeed > rb.linearVelocity.y ? glideFallSpeed : rb.linearVelocity.y);
                }
        }
    }


    public bool CheckGround()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, grondRange, groundLayer);
        if (hit.Length > 0)
        {
            canDash = true;
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, grondRange);
    }

}

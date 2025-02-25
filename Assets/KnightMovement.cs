using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private Rigidbody2D rb;
    private Vector2 movement;
    bool isFlipped;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFlipped = gameObject.GetComponent<KnightAI>().isFlipped;
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the player
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (moveInput > 0 && isFlipped)
        {
            Flip();
        }
        else if (moveInput < 0 && !isFlipped)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }

        movement = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isFlipped = !isFlipped;
    }

}
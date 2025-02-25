using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private Rigidbody2D rb;
    private Vector2 movement;
    bool isFlipped;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        isFlipped = gameObject.GetComponent<BatSmallAI>().isFlipped;
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDead", false);
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        if (moveInputX > 0 && isFlipped)
        {
            Flip();
        }
        else if (moveInputX < 0 && !isFlipped) {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }

        movement = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
        rb.linearVelocity = movement;
    }

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isFlipped = !isFlipped;
    }
}

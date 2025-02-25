using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MOTION : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public GameObject globalStates;
    public dashTimer dashtimer;
    public GlideTimer glideTimer;

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
    public float glideDuration = 5f;
    private float glideTime = 0f;

    private bool isMoving = false;
    private bool isGliding = false;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = dashRate;
        dashtimer.SetMaxDuration(dashRate);
        glideTimer.SetMaxDuration(glideDuration);
    }

    // Update is called once per frame
    void Update()
    {
        dashTime -= math.max(-1, Time.deltaTime);

        bool wasMoving = isMoving;
        isMoving = false;

        bool wasGliding = isGliding;
        isGliding = false;

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector2(-moveForce, 0), ForceMode2D.Force);
            if (dashTime < dashRate - dashDuration) rb.linearVelocity = new Vector2(-math.min(speed, math.abs(rb.linearVelocityX)), rb.linearVelocityY);
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
            {
                rb.AddForce(new Vector2(-dashForce, 0), ForceMode2D.Impulse);
                canDash = false;
                dashTime = dashRate;
                dashtimer.StartTimer();
                audioManager.PlaySFX(audioManager.slimeDash, 0.5f);
            }
            isMoving = true;


        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector2(moveForce, 0), ForceMode2D.Force);
            if (dashTime < dashRate - dashDuration) rb.linearVelocity = new Vector2(math.min(speed, rb.linearVelocityX), rb.linearVelocityY);
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && dashTime <= 0)
            {
                rb.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
                canDash = false;
                dashTime = dashRate;
                dashtimer.StartTimer();
                audioManager.PlaySFX(audioManager.slimeDash, 0.5f);
            }
            isMoving = true;

        }
        if (CheckGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                audioManager.PlaySFX(audioManager.slimeJump, 0.5f);
            }
            glideTime = glideDuration;
            glideTimer.SetTimer(glideDuration - glideTime);
        }
        else
        {
            if (globalStates.GetComponent<Abilities>().canGlide && glideTime >= 0f)
                if (Input.GetKey(KeyCode.Space))
                {
                    glideTime = math.max(-1, glideTime - Time.deltaTime);
                    glideTimer.SetTimer(glideDuration - glideTime);
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, glideFallSpeed > rb.linearVelocity.y ? glideFallSpeed : rb.linearVelocity.y);
                    isGliding = true;
                }
        }

        if(isGliding && !wasGliding)
        {
            audioManager.PlaySFX(audioManager.slimeGlide, 0.5f);
        }
        else if (!isGliding && wasGliding)
        {
            audioManager.SFXsource.Stop();
        }

        if (isMoving && !wasMoving)
        {
            audioManager.PlaySFX(audioManager.slimeWalking1,0.6f);
        }
        else if (!isMoving && wasMoving)
        {
           audioManager.SFXsource.Stop();
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

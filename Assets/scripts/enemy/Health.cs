using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;

    public bool isDead = false;

    public int currentHealth = 100;

    public float alertRadius = 10f;

    public Animator animator;

    public float blinkDuration = 0.1f;

    public int blinkCount = 5;

    public PlayerPosition playerController;
    public GameObject victoryPanel;

    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;

    [Header("--------For Big knight only--------")]
    public LayerMask playerLayer;
    public float detectionRadius = 10f;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private bool player = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
        if(gameObject.name == "BigKnight")
        {
            gameObject.GetComponent<KnightAI>().enabled = false;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead",true);
        }
        if (gameObject.name == "KNIGHT(KEY)")
        {
            currentHealth = 0;
            Die();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(healthBar != null) healthBar.SetHealth(currentHealth);
        if (gameObject.name == "BigKnight" && player == false && isDead == false) {
            Collider2D hit = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y-10f), detectionRadius, playerLayer);
            if (hit!=null)
            {
                player = true;
                gameObject.GetComponent<KnightAI>().enabled = true;
                animator.SetBool("isAwake", true);
                animator.SetBool("isDead", false);
                gameObject.GetComponent<KnightAI>().isPursuing = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(gameObject.GetComponent<KnightAI>() != null)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, alertRadius);

            foreach (Collider2D x in hit)
            {
                if(x.gameObject.GetComponent<KnightAI>())
                {
                    x.gameObject.GetComponent<KnightAI>().player = playerController.currentPlayer;
                    x.gameObject.GetComponent<KnightAI>().isPursuing = true;
                }
            }
        }
        if (gameObject.GetComponent<SpiderSmallAI>() != null) { 
            gameObject.GetComponent<SpiderSmallAI>().player = playerController.currentPlayer;
            gameObject.GetComponent<SpiderSmallAI>().isPursuing = true;
        }
        if (gameObject.GetComponent<BatSmallAI>() != null) { 
            gameObject.GetComponent<BatSmallAI>().player = playerController.currentPlayer;
            gameObject.GetComponent<BatSmallAI>().isPursuing = true;
        }
        healthBar.SetHealth(currentHealth);
        if (!isDead)
        {
            StartCoroutine(BlinkEffect());
        }
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die() {
        if(gameObject.CompareTag("CurrentPlayer")){
            if(gameObject.GetComponent<Health>() != null)
            {
                gameObject.GetComponent<Health>().isDead = true;
                if(gameObject.GetComponent<MOTION>() != null) gameObject.GetComponent<MOTION>().enabled = false;
                GameObject.Find("GameManager").GetComponent<GameManager>().DeathMenu();
            }
        }
        if (gameObject.GetComponent<KnightAI>() != null) {
            audioManager.PlaySFX(audioManager.knightDeath, 0.5f);
            gameObject.GetComponent<KnightAI>().enabled = false;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead", true);
            isDead = true;
        }
        if (gameObject.GetComponent<MOTION>() != null) {
            gameObject.GetComponent<MOTION>().enabled = false;
            isDead = true;
            Destroy(gameObject);
        }
        if(gameObject.GetComponent<SpiderSmallAI>() != null)
        {
            gameObject.GetComponent<SpiderSmallAI>().enabled = false;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead", true);
            isDead = true;
        }
        if (gameObject.GetComponent<BatSmallAI>()) {
            gameObject.GetComponent <BatSmallAI>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead", true);
            isDead = true;
        }
        if (gameObject.GetComponent<BigSpiderAI>()) {
            gameObject.GetComponent<BigSpiderAI>().enabled = false;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead", true);
            isDead = true;
        }
        if (gameObject.GetComponent<Chandalier>())
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if (gameObject.GetComponent<DragonCombat>() != null)
        {
            gameObject.GetComponent<DragonCombat>().enabled = false;
            animator.SetTrigger("deadAnim");
            animator.SetBool("isDead", true);
            isDead = true;
            Invoke("Victory", 2f);
        }

        isDead = true;
  
    }

    void Victory(){
        victoryPanel.GetComponent<Victory>().OnVictory();
    }

    private IEnumerator BlinkEffect()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, alertRadius);
        if(gameObject.name == "BigKnight")
        {
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - 10f), detectionRadius);
        }
    }
}
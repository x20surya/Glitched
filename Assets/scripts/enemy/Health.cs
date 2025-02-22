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

    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

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

        if (gameObject.GetComponent<KnightAI>() != null) {
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
    }
}
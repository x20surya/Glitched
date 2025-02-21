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

    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                    x.gameObject.GetComponent<KnightAI>().isPursuing = true;
                }
            }
        }
        StartCoroutine(BlinkEffect());
        if (currentHealth <= 0)
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
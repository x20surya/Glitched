using System.Collections;
using UnityEngine;

public class BigSpiderAI : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float pursuingSpeed;
    [SerializeField] public float pursuingTime = 3f;
    private Rigidbody2D rb;
    public Transform visionCheck;
    [SerializeField] public float visionDistance = 15f;
    GameObject currentTarget;
    public bool isFlipped = false;
    public bool isPursuing = false;
    [SerializeField] public float[] patrolPointsX;
    [SerializeField] public float patrolingSpeed = 5f;
    int currentPointIndex;
    bool once;
    private Coroutine stopPursuingCoroutine;
    public float attackRange = 3f;
    public Animator animator;
    public LayerMask spiderCanAttackLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget)
        {
            if (currentTarget.gameObject.GetComponent<Health>().isDead)
            {
                isPursuing = false;
                currentTarget = null;
            }
        }

        if (currentTarget == null)
        {
            isPursuing = false;
        }

        if (isPursuing && currentTarget)
        {
            Pursuing();
        }
        else if (!isPursuing)
        {
            Patrol();
            DetectEnemy();
        }

        


    }

    void Patrol()
    {
        if (transform.position != new Vector3(patrolPointsX[currentPointIndex], transform.position.y, transform.position.z))
        {

            rb.linearVelocity = Vector3.zero;

            if (transform.position.x > patrolPointsX[currentPointIndex] && !isFlipped)
            {
                Flip();
            }
            else if (transform.position.x < patrolPointsX[currentPointIndex] && isFlipped)
            {
                Flip();
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolPointsX[currentPointIndex], transform.position.y), patrolingSpeed * Time.deltaTime);

        }
        else
        {

            if (once == false)
            {
                once = true;

                if (currentPointIndex + 1 < patrolPointsX.Length)
                {
                    currentPointIndex++;
                }
                else
                {
                    currentPointIndex = 0;
                }

                once = false;
            }

        }
    }

    void Pursuing()
    {
        LookAtPlayer();

        if (Vector2.Distance(currentTarget.transform.position, rb.position) <= attackRange)
        {

            //Attack
            rb.linearVelocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
        else
        {
            rb.linearVelocity = new Vector2(pursuingSpeed, rb.linearVelocity.y);
        }
    }

    void LookAtPlayer()
    {

        if (transform.position.x > currentTarget.transform.position.x && !isFlipped)
        {
            Flip();
        }
        else if (transform.position.x < currentTarget.transform.position.x && isFlipped)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isFlipped = !isFlipped;
        speed *= -1;
        pursuingSpeed *= -1;
    }

    void DetectEnemy()
    {
        bool targetDetected = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(visionCheck.position, visionDistance, spiderCanAttackLayer);

        foreach (var hit in hits)
        {

            if (hit.gameObject.GetComponent<Health>() != null)
            {
                if (hit.gameObject != gameObject && !hit.gameObject.GetComponent<Health>().isDead)
                {

                    if (!hit.gameObject.GetComponent<Health>().isDead)
                    {
                        currentTarget = hit.gameObject;
                        isPursuing = true;
                        targetDetected = true;

                        if (stopPursuingCoroutine != null)
                        {
                            StopCoroutine(stopPursuingCoroutine);
                            stopPursuingCoroutine = null;
                        }
                        break;
                    }
                }
            }
        }

        if (!targetDetected && isPursuing && stopPursuingCoroutine == null)
        {
            stopPursuingCoroutine = StartCoroutine(StopPursuingAfterDelay(pursuingTime));
        }

    }

    IEnumerator StopPursuingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPursuing = false;
        currentTarget = null;
    }

}

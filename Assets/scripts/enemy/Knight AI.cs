using System.Collections;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
    [SerializeField] public float speed;

    [SerializeField] public float pursuingSpeed;

    [SerializeField] public float pursuingTime = 3f;

    public Transform visionCheck;

    [SerializeField] public float visionDistance = 15f;

    [SerializeField] public float visionAngle = 45f;

    private Rigidbody2D rb;

    [SerializeField] public LayerMask playerLayer;

    public Transform player;

    bool isFlipped = false;

    bool isPursuing = false;

    [SerializeField] public float[] patrolPointsX;

    [SerializeField] public float patrolingSpeed = 5f;

    [SerializeField] public Transform firePoint;

    [SerializeField] public GameObject bullet;

    [SerializeField] public float bulletInterval = 1f;

    int currentPointIndex;

    bool once;

    private Coroutine stopPursuingCoroutine;

    private Coroutine bulletCoroutine;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {
        Debug.Log(isPursuing);

        if (isPursuing == false)
        {
            //RaycastHit2D patrolCheckinfo = Physics2D.Raycast(patrolCheck.position, -Vector2.up, patrolCheckvisionDistance);

            //if (patrolCheckinfo.collider != null)
            //{
            //    rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            //}
            //else
            //{
            //    Flip();
            //}

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
        else if (isPursuing)
        {
            LookAtPlayer();
            if (player.position.y - transform.position.y > 3f)
            {
                rb.linearVelocity = Vector2.zero;

                if (bulletCoroutine == null)
                {
                    bulletCoroutine = StartCoroutine(FireBulletAtIntervals());
                }

            }
            else
            {
                rb.linearVelocity = new Vector2(pursuingSpeed, rb.linearVelocity.y);
            }

        }

        DetectInCone();

    }

    IEnumerator FireBulletAtIntervals()
    {

        float angleBWPlayer = (180 / Mathf.PI) * Mathf.Atan2((player.position.y - transform.position.y), (player.position.x - transform.position.x));
        Instantiate(bullet, firePoint.position, Quaternion.Euler(0, 0, angleBWPlayer));
        yield return new WaitForSeconds(bulletInterval);
        bulletCoroutine = null;

    }

    void LookAtPlayer()
    {

        if (transform.position.x > player.position.x && !isFlipped)
        {
            Flip();
        }
        else if (transform.position.x < player.position.x && isFlipped)
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

    void DetectInCone()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(visionCheck.position, visionDistance, playerLayer);

        bool playerDetected = false;

        foreach (Collider2D hit in hits)
        {
            Vector2 directionToTarget = (hit.transform.position - visionCheck.position).normalized;
            float angleToTarget = Vector2.Angle(visionCheck.right, directionToTarget);

            if (angleToTarget < visionAngle / 2)
            {
                Debug.Log("Detected object within cone: " + hit.gameObject.name);

                Vector2 direction = hit.gameObject.transform.position - visionCheck.position;

                RaycastHit2D visionCheckinfo = Physics2D.Raycast(visionCheck.position, direction, visionDistance - 1f);
                Debug.DrawLine(visionCheck.position, visionCheckinfo.point);

                if (visionCheckinfo.collider)
                {
                    if (visionCheckinfo.collider.gameObject.layer == 3)
                    {
                        Debug.Log("Player Detected");
                        isPursuing = true;
                        playerDetected = true;

                        if (stopPursuingCoroutine != null)
                        {
                            StopCoroutine(stopPursuingCoroutine);
                            stopPursuingCoroutine = null;
                        }
                        break;
                    }
                }

                // Add your logic for when an object is detected within the cone
            }
        }


        if (!playerDetected && isPursuing && stopPursuingCoroutine == null)
        {
            stopPursuingCoroutine = StartCoroutine(StopPursuingAfterDelay(pursuingTime));
        }
    }

    IEnumerator StopPursuingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isPursuing = false;

        Debug.Log("Stopped pursuing player after delay");
    }



    private void OnDrawGizmos()
    {
        if (visionCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(visionCheck.position, visionDistance);

            Vector3 leftBoundary = Quaternion.Euler(0, 0, visionAngle / 2) * visionCheck.right * visionDistance;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, -visionAngle / 2) * visionCheck.right * visionDistance;

            Gizmos.DrawLine(visionCheck.position, visionCheck.position + leftBoundary);
            Gizmos.DrawLine(visionCheck.position, visionCheck.position + rightBoundary);
        }
    }

}

using Unity.Cinemachine;
using UnityEngine;

public class possess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform slime;
    public float possessRange = 8f;
    public LayerMask EnemyLayer;

    public PlayerPosition playerController;
    void Start()
    {
        slime = GameObject.Find("main_body").transform;
        EnemyLayer = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            checkForPossess();
        }
    }

    void checkForPossess()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(slime.transform.position, possessRange, EnemyLayer);
        if (hitColliders.Length == 0)
        {
            return;
        }
        Collider2D closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider2D hitCollider in hitColliders)
        {
            float temp = Vector2.Distance(hitCollider.transform.position, slime.position);
            if (temp < closestDistance)
            {
                closestDistance = temp;
                closestEnemy = hitCollider;
            }
        }
        Possess(closestEnemy);
    }

    void Possess(Collider2D hitCollider)
    {

        if (hitCollider.gameObject.GetComponent<Health>().isDead)
        {
            Debug.Log("Possess: " + hitCollider.name);
            // enable move script in enemy
            if (hitCollider.gameObject.GetComponent<MOTION>())
            {
                hitCollider.gameObject.GetComponent<MOTION>().enabled = true;
            }
            if (hitCollider.gameObject.GetComponent<KnightMovement>())
            {
                hitCollider.gameObject.GetComponent<KnightMovement>().enabled = true;
                hitCollider.gameObject.GetComponent<Health>().currentHealth = hitCollider.gameObject.GetComponent<Health>().maxHealth;
                playerController.currentPlayer = hitCollider.gameObject;
                playerController.isPossesed = true;
                hitCollider.gameObject.tag = "CurrentPlayer";

            }
            hitCollider.gameObject.GetComponent<Unpossess>().enabled = true;

            hitCollider.gameObject.GetComponent<Rigidbody2D>().mass = 6f; // 5f -> weight of balls

            // shift camera to enemy
            CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
            if (vcam != null)
            {
                vcam.Follow = hitCollider.gameObject.transform;
                vcam.LookAt = hitCollider.gameObject.transform;
            }
            // GetComponent<CinemachineCamera>().Target.TrackingTarget = hitCollider.gameObject.transform;

            // disable move script in slime

            if (slime.gameObject.GetComponent<MOTION>() != null)
            {
                slime.gameObject.GetComponent<MOTION>().enabled = false;
            }


            hitCollider.gameObject.GetComponent<Unpossess>().isPossessed = true;
            hitCollider.gameObject.GetComponent<Unpossess>().slime = gameObject;


            // disable slime
            gameObject.SetActive(false);


        }
    }
}

// void OnDrawGizmosSelected()
// {
//     if(slime == null)
//     {
//         return;
//     }
//     Gizmos.DrawWireSphere(slime.transform.position, possessRange);
// }



using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class possess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform slime;
    public float possessRange = 8f;
    public LayerMask EnemyLayer;
    public float possessRate = 3f;
    private float possessTime = 0f;

    public PlayerPosition playerController;
    public possessTimer possessTimer;
    public GameObject gameManager;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        slime = GameObject.Find("main_body").transform;
        EnemyLayer = LayerMask.GetMask("Enemy");
        possessTimer.SetMaxDuration(possessRate);
    }

    // Update is called once per frame
    void Update()
    {
        possessTime = math.max(-1, possessTime - Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E) && possessTime <= 0)
        {
            checkForPossess();
        }
    }
    public void SetPossessTimer()
    {
        possessTime = possessRate;
        possessTimer.SetMaxDuration(possessRate);
        possessTimer.StartTimer();
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
            audioManager.PlaySFX(audioManager.slimePossess, 1f);
            // enable move script in enemy
            if (hitCollider.gameObject.GetComponent<MOTION>())
            {
                hitCollider.gameObject.GetComponent<MOTION>().enabled = true;
            }
            if (hitCollider.gameObject.GetComponent<KnightMovement>())
            {
                hitCollider.gameObject.GetComponent<KnightMovement>().enabled = true;
                hitCollider.gameObject.GetComponent<Health>().currentHealth = hitCollider.gameObject.GetComponent<Health>().maxHealth;
                hitCollider.gameObject.GetComponent<Health>().isDead = false;
                playerController.currentPlayer = hitCollider.gameObject;
                playerController.isPossesed = true;
                hitCollider.gameObject.tag = "CurrentPlayer";

            }
            if (hitCollider.gameObject.GetComponent<SpiderMovement>())
            {
                hitCollider.gameObject.GetComponent<SpiderMovement>().enabled = true;
                hitCollider.gameObject.GetComponent<WebSwing>().enabled = true;
                hitCollider.gameObject.GetComponent<Health>().currentHealth = hitCollider.gameObject.GetComponent<Health>().maxHealth;
                hitCollider.gameObject.GetComponent<Health>().isDead = false;
                playerController.currentPlayer = hitCollider.gameObject;
                playerController.isPossesed = true;
                hitCollider.gameObject.tag = "CurrentPlayer";
            }
            if (hitCollider.gameObject.GetComponent<BatMovement>())
            {
                hitCollider.gameObject.GetComponent<BatMovement>().enabled = true;
                hitCollider.gameObject.GetComponent<Health>().currentHealth = hitCollider.gameObject.GetComponent<Health>().maxHealth;
                hitCollider.gameObject.GetComponent<Health>().isDead = false;
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
            hitCollider.gameObject.GetComponent<Unpossess>().StartUnpossessTimer();
            hitCollider.gameObject.GetComponent<Unpossess>().spwawnoffset = gameObject.transform.position;

            // if(hitCollider.gameObject.GetComponent<Health>() != null)
            // {
            //     hitCollider.gameObject.GetComponent<Health>().SetMaxHealth();
            // }

            //checking if opponent has key
            if(hitCollider.GetComponent<Key>())
            {
                gameManager.GetComponent<GameManager>().GotKey();
            }



            // disable slime
            gameObject.transform.position = Vector3.zero;
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



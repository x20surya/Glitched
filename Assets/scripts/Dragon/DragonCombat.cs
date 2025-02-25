using System.Collections;
using UnityEngine;

public class DragonCombat : MonoBehaviour
{

    public float rockSlideCooldown = 10f;
    public float flameThrowerCooldown = 5f;
    public float tailWhipCooldown = 7f;
    public float platformCooldown1 = 8f;
    public float platformCooldown2 = 12f;
    public float platformCooldown3 = 10f;
    public float platformCooldown4 = 6f;

    private float rockSlideTimer;
    private float flameThrowerTimer;
    private float tailWhipTimer;
    private float platformTimer1;
    private float platformTimer2;
    private float platformTimer3;
    private float platformTimer4;

    private Transform tempPlayerPos;

    public GameObject rockPrefab;
    public Transform rockSpawnPoint;
    public GameObject flamePrefab;
    public Transform flameSpawnPoint;
    public GameObject eruptionPrefab;
    public Transform eruptionSpawnPoint;
    public GameObject platformPrefab;
    public Transform platformSpawnPoint1;
    public Transform platformSpawnPoint2;
    public Transform platformSpawnPoint3;
    public Transform platformSpawnPoint4;
    public Animator animator;

    Coroutine playerPos;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rockSlideTimer = rockSlideCooldown;
        flameThrowerTimer = flameThrowerCooldown;
        tailWhipTimer = tailWhipCooldown;
        platformTimer1 = platformCooldown1;
        platformTimer2 = platformCooldown2;
        platformTimer3 = platformCooldown3;
        platformTimer4 = platformCooldown4;
    }

    void Update()
    {
        if(playerPos == null)
        {
            playerPos = StartCoroutine(PlayerPos());
        }
        rockSlideTimer -= Time.deltaTime;
        flameThrowerTimer -= Time.deltaTime;
        tailWhipTimer -= Time.deltaTime;
        platformTimer1 -= Time.deltaTime;
        platformTimer2 -= Time.deltaTime;
        platformTimer3 -= Time.deltaTime;
        platformTimer4 -= Time.deltaTime;

        if (rockSlideTimer <= 0)
        {
            
            animator.SetTrigger("rockSlide");
            rockSlideTimer = rockSlideCooldown;
        }

        if (flameThrowerTimer <= 0)
        {
            
            animator.SetTrigger("flameThrower");
            flameThrowerTimer = flameThrowerCooldown;
        }

        if (tailWhipTimer <= 0)
        {
            
            animator.SetTrigger("eruption");
            tailWhipTimer = tailWhipCooldown;
        }
        if (platformTimer1 <= 0)
        {
            Instantiate(platformPrefab, platformSpawnPoint1.position, Quaternion.identity);
            platformTimer1 = platformCooldown1;
        }
        if (platformTimer2 <= 0)
        {
            audioManager.PlaySFX(audioManager.dragonGrowl, 1f);
            Instantiate(platformPrefab, platformSpawnPoint2.position, Quaternion.identity);
            platformTimer2 = platformCooldown2;
        }
        if (platformTimer3 <= 0)
        {
            Instantiate(platformPrefab, platformSpawnPoint3.position, Quaternion.identity);
            platformTimer3 = platformCooldown3;
        }
        if (platformTimer4 <= 0)
        {
            Instantiate(platformPrefab, platformSpawnPoint4.position, Quaternion.identity);
            platformTimer4 = platformCooldown4;
        }
    }

    public void RockSlide()
    {
        audioManager.PlaySFX(audioManager.dragonDuringRockSlide, 1f);

        Instantiate(rockPrefab, new Vector2(tempPlayerPos.position.x - 20f, rockSpawnPoint.position.y), Quaternion.identity);
        Instantiate(rockPrefab, new Vector2(tempPlayerPos.position.x - 10f, rockSpawnPoint.position.y), Quaternion.identity);
        Instantiate(rockPrefab, new Vector2(tempPlayerPos.position.x + 20f, rockSpawnPoint.position.y), Quaternion.identity);
        Instantiate(rockPrefab, new Vector2(tempPlayerPos.position.x + 10f, rockSpawnPoint.position.y), Quaternion.identity);

        
        Debug.Log("Rock Slide attack executed");
    }

    public void FlameThrower()
    {
        audioManager.PlaySFX(audioManager.flameThrower, 1f);
        float angleBWPlayer = (180 / Mathf.PI) * Mathf.Atan2((GameObject.FindGameObjectWithTag("CurrentPlayer").transform.position.y - flameSpawnPoint.position.y), (GameObject.FindGameObjectWithTag("CurrentPlayer").transform.position.x - flameSpawnPoint.position.x));
        // Placeholder logic for Flame Thrower attack
        Instantiate(flamePrefab, flameSpawnPoint.position, Quaternion.Euler(0, 0, angleBWPlayer));

        

        Debug.Log("Flame Thrower attack executed");
    }

    public void TailWhip()
    {
        audioManager.PlaySFX(audioManager.dragonEruption, 1f);

        Instantiate(eruptionPrefab, new Vector2(tempPlayerPos.position.x - 40f, eruptionSpawnPoint.position.y), Quaternion.identity);
        Instantiate(eruptionPrefab, new Vector2(tempPlayerPos.position.x - 20f, eruptionSpawnPoint.position.y), Quaternion.identity);
        Instantiate(eruptionPrefab, new Vector2(tempPlayerPos.position.x + 40f, eruptionSpawnPoint.position.y), Quaternion.identity);
        Instantiate(eruptionPrefab, new Vector2(tempPlayerPos.position.x + 20f, eruptionSpawnPoint.position.y), Quaternion.identity);
        // Placeholder logic for Tail Whip attack
        
        Debug.Log("Tail Whip attack executed");
    }

    IEnumerator PlayerPos()
    {
        tempPlayerPos = GameObject.FindGameObjectWithTag("CurrentPlayer").transform;
        yield return new WaitForSeconds(rockSlideCooldown);
        playerPos = null;
    }
}

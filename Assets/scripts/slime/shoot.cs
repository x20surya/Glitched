using UnityEngine;

public class shoot : MonoBehaviour
{
    // bullet prefab
    public GameObject bullet;
    public GameObject rock;
    public float bulletSpeed = 35f;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public float rockSpeed = 10f;
    public float rockRate = 0.5f;
    private float nextRock = 0.0f;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        nextFire = fireRate;
        nextRock = rockRate;
    }
    // Update is called once per frame
    void Update()
    {
        nextFire -= Time.deltaTime;
        // attcak when left click
        if (Input.GetMouseButtonDown(0))
        {
            if (nextFire <= 0 && GameObject.Find("Global State").GetComponent<Abilities>().canShoot)
            {
                Fire();
            }
        }
        nextRock -= Time.deltaTime;
        // attcak when right click
        if (Input.GetMouseButtonDown(1))
        {
            if (nextRock <= 0)
            {
                FireRock();
            }
        }
    }

    void Fire()
    {// get the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get the player position
        Vector2 playerPos = transform.position;
        // get the direction
        Vector2 direction = mousePos - playerPos;
        // get the angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // create the bullet
        GameObject bulletClone = Instantiate(bullet, playerPos, Quaternion.Euler(0, 0, angle));
        // set the bullet direction
        bulletClone.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bulletSpeed;
        nextFire = fireRate;
        audioManager.PlaySFX(audioManager.slimeSpit, 0.5f);

    }
    void FireRock()
    {// get the mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get the player position
        Vector2 playerPos = transform.position;
        // get the direction
        Vector2 direction = mousePos - playerPos;
        // get the angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // create the bullet
        GameObject rockClone = Instantiate(rock, playerPos, Quaternion.Euler(0, 0, angle));
        // set the bullet direction
        rockClone.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * rockSpeed;

        nextRock = rockRate;
    }
}

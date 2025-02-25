using UnityEngine;
using Unity.Cinemachine;
using Unity.Mathematics;

public class Unpossess : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isPossessed = false;
    public GameObject slime;
    public PlayerPosition playerController;
    public float possessDuration = 8f;
    private float possessTime = 8f;
    public possessTimer possessTimer;
    public Vector3 spwawnoffset;
    


    public void StartUnpossessTimer()
    {
        possessTimer.SetMaxDuration(possessDuration);
        possessTimer.StartTimer();
        possessTime = possessDuration;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (isPossessed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnPossess();
            }
            if (possessTime <= 0)
            {
                UnPossess();
            }
            possessTime = math.max(-1, possessTime - Time.deltaTime);
        }

    }

    void UnPossess()
    {
        // Ensure slime is valid
        if (slime == null)
        {
            return;
        }

        // Debugging: Print positions

        // Ensure Slime has no parent
        slime.transform.SetParent(null);

        // Apply spawn offset correctly
        // Log initial values

        // Set slime position
        slime.transform.position = spwawnoffset;

        // Log new slime position

        slime.transform.rotation = Quaternion.identity; // Reset rotation
        slime.transform.localScale = Vector2.one;  // Ensure correct scale

        // Debugging before activation

        // Handle Rigidbody2D if present
        Rigidbody2D rb = slime.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;  // Temporarily disable physics
        }

        // Activate slime
        slime.SetActive(true);

        // Debugging after activation
        // Restore physics if needed
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        playerController.currentPlayer = GameObject.Find("main_body");

        // Enable move script in slime
        MOTION motion = GameObject.Find("main_body").GetComponent<MOTION>();
        if (motion != null)
        {
            motion.enabled = true;
            playerController.isPossesed = false;
        }

        // Activate possess script if present
        possess possessScript = slime.GetComponent<possess>();
        if (possessScript != null)
        {
            possessScript.SetPossessTimer();
        }

        // Adjust camera to follow slime
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();
        if (vcam != null)
        {
            GameObject mainBody = GameObject.Find("main_body");
            vcam.Follow = mainBody.transform;
            vcam.LookAt = mainBody.transform;
        }

        // Restore some health to main_body if it has a Health component
        Health health = GameObject.Find("main_body").GetComponent<Health>();
        if (health != null)
        {
            health.currentHealth += 20;
            playerController.isPossesed = false;
        }

        // Destroy enemy object **after** positioning is complete
        Destroy(gameObject);
    }

}

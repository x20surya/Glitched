using UnityEngine;

public class WebSwing : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    public Rigidbody2D rb;

    public float grappleforce = 10f;

    private Vector2 grapplePoint;
    public SpringJoint2D joint;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<SpringJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            RaycastHit2D hit = Physics2D.Raycast(
            origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
            direction: Vector2.zero,
            distance: Mathf.Infinity,
            layerMask: grappleLayer
            );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
            }

            audioManager.PlaySFX(audioManager.slimeWebSwing, 0.5f);

            //if(grapplePoint.y - transform.localPosition.y >=0)
            //{

            //    rb.linearVelocity = new Vector2(100f, rb.linearVelocity.y);

            //}
            //else
            //{

            //    rb.linearVelocity = new Vector2(-100f, rb.linearVelocity.y);

            //}

        }

        if (Input.GetMouseButtonUp(2)) // Middle mouse button up
        {
            joint.enabled = false;
            rope.enabled = false;
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }



    }


}

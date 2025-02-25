using UnityEngine;

public class recoil : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddRecoil(){
        Vector2 forceDirection = transform.position - GameObject.Find("main_body").transform.position;
        rb.AddForce( forceDirection / 25, ForceMode2D.Impulse);
    }
}

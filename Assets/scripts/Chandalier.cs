using UnityEngine;

public class Chandalier : MonoBehaviour
{
    public int damage = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("CurrentPlayer"))
        {
            if (collision.gameObject.GetComponent<Health>()){
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

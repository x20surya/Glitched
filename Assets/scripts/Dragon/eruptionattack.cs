using System.Collections;
using UnityEngine;

public class eruptionattack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int damage = 20;

    void Start()
    {
        StartCoroutine(destroyGameObject());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CurrentPlayer"))
        {
            if(collision.GetComponent<Health>() != null)
                collision.GetComponent<Health>().TakeDamage(damage);
        }

    }

    IEnumerator destroyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}

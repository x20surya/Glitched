using System.Collections;
using UnityEngine;

public class EruptionScript : MonoBehaviour
{
    public GameObject ringPrefab1;
    public GameObject ringPrefab2;
    public GameObject firePrefab;
    public float fireSpawnTime = 3f;
    public float totalEruptionTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(ringPrefab1,transform.position, Quaternion.identity);
        GameObject ring2 = Instantiate(ringPrefab2 ,transform.position, Quaternion.identity);
        StartCoroutine(spawnFire());
        StartCoroutine(ShrinkRing(ring2));
        StartCoroutine(destroyGameObject());


    }

    private void Update()
    {
        
    }

    IEnumerator spawnFire()
    {
        yield return new WaitForSeconds(fireSpawnTime);
        Instantiate(firePrefab,new Vector3(transform.position.x, transform.position.y+8f, transform.position.z), Quaternion.identity);
    }

    IEnumerator ShrinkRing(GameObject ring)
    {
        Vector3 originalScale = ring.transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < fireSpawnTime)
        {
            ring.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / fireSpawnTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ring.transform.localScale = Vector3.zero;
    }

    IEnumerator destroyGameObject()
    {
        yield return new WaitForSeconds(totalEruptionTime);
        Destroy(gameObject);
    }

}

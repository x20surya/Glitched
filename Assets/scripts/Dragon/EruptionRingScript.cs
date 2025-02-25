using System.Collections;
using UnityEngine;

public class EruptionRingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(destroyGameObject());
    }

    // Update is called once per frame
    IEnumerator destroyGameObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

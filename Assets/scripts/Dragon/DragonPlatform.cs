using System.Collections;
using UnityEngine;

public class DragonPlatform : MonoBehaviour
{
    public float destroyPlatformAfter = 5f;
    public float shakePlatformFor = 2f;
    private Vector3 originalPosition;
    private bool isShaking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(DestroyPlatform());
        StartCoroutine(ShakePlatform());
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            Shake();
        }
    }

    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(destroyPlatformAfter);
        Destroy(gameObject);
    }

    IEnumerator ShakePlatform()
    {
        yield return new WaitForSeconds(destroyPlatformAfter - shakePlatformFor);
        isShaking = true;
        yield return new WaitForSeconds(shakePlatformFor);
        isShaking = false;
        transform.position = originalPosition;
    }

    void Shake()
    {
        float shakeAmount = 0.1f;
        Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeAmount;
        shakePosition.z = originalPosition.z; // Keep the original z position
        transform.position = shakePosition;
    }
}

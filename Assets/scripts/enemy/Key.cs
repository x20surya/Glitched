using UnityEngine;

public class Key : MonoBehaviour
{
    void Start()
    {
        GameObject Key = gameObject.transform.Find("ENEMY HUD").transform.Find("KeyImage").gameObject;
        Key.SetActive(true);
    }
}

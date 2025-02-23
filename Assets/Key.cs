using UnityEngine;

public class Key : MonoBehaviour
{
    void Update()
    {
        if(gameObject.GetComponent<Health>().isDead){
            GameObject.Find("Global State").GetComponent<Abilities>().hasKey = true;
            Destroy(gameObject);
        }
    }
}

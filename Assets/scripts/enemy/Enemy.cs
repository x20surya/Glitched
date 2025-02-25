
using UnityEngine;

// this script stores the common characteristics of all enemy

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isDead = false;

    void Start()
    {
        isDead = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    

}

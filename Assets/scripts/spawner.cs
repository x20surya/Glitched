using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject spawn;

    public int maxSpawns = 5;
    public float radius = 5f;
    private float spawnTime;
    public float spawnRate = 2f;
    void Start()
    {
        spawnTime = spawnRate;
        
    }

    // Update is called once per frame
    void Update()
    {
        // spawnTime -= Time.deltaTime;
        // spawn.GetComponent<rochThrow>().enabled = false;
        // if (spawnTime <= 0)
        // {
        //     if (GameObject.FindGameObjectsWithTag(spawn.tag).Length <= maxSpawns)
        //     {

        //         Vector3 temp = new Vector3(Random.Range(-radius, radius), 0, 0);
        //         Vector3 spawnPosition = transform.TransformPoint(temp);
        //         Instantiate(spawn, spawnPosition, Quaternion.identity);
                
        //     }
        //     spawnTime = spawnRate;
        // }
    }
}

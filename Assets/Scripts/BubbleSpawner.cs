using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] GameObject bubbles;
    [SerializeField] float spawnFrequency = 1f;
    [SerializeField] float timeToSpawn = 1;
    [SerializeField] Vector3 spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpawn < 0)
        {
            Instantiate(bubbles, transform.position + spawnPosition, transform.rotation, transform);
            timeToSpawn = spawnFrequency;
        }
        else
        {
            timeToSpawn -= Time.deltaTime;
        }
    }
}

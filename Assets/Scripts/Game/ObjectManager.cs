using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnRate = 2f;
    public float spawnRangeX = 8f;
    public float minSpawnY = 5f;
    public float maxSpawnY = 7f;

    private float nextSpawn = 0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        int rand = Random.Range(0, objectsToSpawn.Length);
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(minSpawnY, maxSpawnY);

        Vector2 spawnPosition = new Vector2(spawnPosX, spawnPosY);
        Instantiate(objectsToSpawn[rand], spawnPosition, Quaternion.identity);
    }
}

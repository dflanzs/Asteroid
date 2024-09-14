using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{
    public float maxTimeLife = 4f;
    public float spawnRatePerMinute = 30f;
    public float spawnIncrement = 1f;
    private float spawnDelay;
    public Vector3 targetVector;
    
    public float xLimit;

    public GameObject meteorPrefab;

    void Update()
    {
        if(Time.time > spawnDelay){
            spawnDelay = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnIncrement;

            Vector2 spawnPosition = getRandomSpawnPoint();

            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxTimeLife);
        }
    }

    private Vector2 getRandomSpawnPoint(){
        return new Vector2(Random.Range(-xLimit, xLimit), 8);
    }
}

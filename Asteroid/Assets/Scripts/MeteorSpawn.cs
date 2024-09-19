using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{    
    public float spawnRatePerMinute = 30f;
    
    public float spawnIncrement = 1f;
    
    private float spawnDelay;
    
    public Vector3 targetVector;
    
    public float xLimit;

    void Update()
    {
        if(Time.time > spawnDelay)
        {
            spawnDelay = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnIncrement;

            Vector3 spawnPosition = getRandomSpawnPoint();

            GameObject meteor = ObjectPooling.Instance.requestInstance("Meteor");

            if(meteor != null)
            {                
                meteor.SetActive(true);
                meteor.transform.position = spawnPosition;

                Debug.Log("Meteorito...");
            }
        }
        checkMeteortOutOfBounds();
    }

    private Vector3 getRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-xLimit, xLimit), 8);
    }

    // Para devolver meteoritos a la pool
    private void checkMeteortOutOfBounds()
    {
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject meteor in meteors)
        {
            if(meteor.transform.position.y < -7)
            {
                meteor.SetActive(false);
            }
        }
    }
}

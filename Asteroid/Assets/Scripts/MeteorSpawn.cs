using System.Collections;
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
        if(Time.time > spawnDelay)
        {
            spawnDelay = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnIncrement;

            Vector2 spawnPosition = getRandomSpawnPoint();

            GameObject meteor = objectPooling.Instance.requestInstance();
            
            meteor.SetActive(true);

            if(meteor != null)
            {
                meteor.transform.position = spawnPosition;
                meteor.transform.rotation = Quaternion.identity;
            }


            StartCoroutine(deactivateMeteor(meteor, maxTimeLife));
        }
    }

    private Vector2 getRandomSpawnPoint()
    {
        return new Vector2(Random.Range(-xLimit, xLimit), 8);
    }

    // Desactivar los meteoritos
    private IEnumerator deactivateMeteor(GameObject meteor, float delay)
    {
        yield return new WaitForSeconds(delay);

        if(meteor != null)
        {
            meteor.SetActive(false);
        }
    }
}

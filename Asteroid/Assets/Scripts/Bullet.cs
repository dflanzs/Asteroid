using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;
    /* public float speedMultiplier; */
    public float maxLifeTime = 3f;

    public Vector3 targetVector;
    
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(speed * /* speedMultiplier * */ targetVector * Time.deltaTime);
    }
}

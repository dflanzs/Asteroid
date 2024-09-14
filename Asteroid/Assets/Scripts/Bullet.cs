using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){

            incrementScore();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void incrementScore(){
        Player.SCORE++;
        updateTextScore();
    }

    private void updateTextScore(){
        GameObject canvas = GameObject.FindGameObjectWithTag("UI");
        canvas.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }
}

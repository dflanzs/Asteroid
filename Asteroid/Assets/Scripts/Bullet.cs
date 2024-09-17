using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;

    public float maxLifeTime = 3f;

    public Vector3 targetVector;

    public TextMeshProUGUI text; // El texto legacy no se adecuaba al tamaño de la pantalla 

    void Start()
    {
        text = FindObjectOfType<TextMeshProUGUI>();
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(speed * /* speedMultiplier * */ targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {

            incrementScore();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void incrementScore()
    {
        Player.SCORE++;
        updateTextScore();
    }

    private void updateTextScore()
    {
        text.text = "Score:" + Player.SCORE;
    }
}

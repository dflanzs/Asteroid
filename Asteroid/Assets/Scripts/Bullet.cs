using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;

    public Vector3 targetVector;

    public TextMeshProUGUI text; // El texto legacy no se adecuaba al tamaño de la pantalla 

    public Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        text = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Enemy"))
        {
            incrementScore();

            collider.gameObject.SetActive(false);
            gameObject.SetActive(false);
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

    public void shoot(Vector3 targetVector, BoxCollider collision)
    {
        rigid.velocity = targetVector * speed;
        collision.gameObject.SetActive(true);
    }
}

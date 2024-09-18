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
        text = FindObjectOfType<TextMeshProUGUI>();
    }

    /* void Update()
    {
        // Space.World para que la bala se mueva respecto del mundo y no respecto de la nave
        transform.Translate(speed * targetVector * Time.deltaTime, Space.World); 
    } */

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            incrementScore();

            Destroy(collision.gameObject);
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

    public void shoot(Vector3 targetVector)
    {
        rigid.velocity = targetVector * speed;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    public static int SCORE;

    // Para el giro de la nave modificamos la componente trasnform
    // Para el movimiento de la nave usamos fuerzas
    public float thrustForce;  // Public permite modificar el valor desde el editor de Unity (Desde el scrript)

    public float rotationSpeed;

    public GameObject gun;

    public TextMeshProUGUI text;

    public float bulletMaxLifeTime = 4f;

    private Rigidbody rigid; // Private impide modificar el valor del copmponente desde el editopr de Unity (Desde el scrript)

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        text = FindObjectOfType<TextMeshProUGUI>();
        // Meteoritos
    }

    void Update()
    {
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime; 
        
        /*
        * Usamos clase input porque es configurable desde Unity, y pq así no tenemos que asignar teclas específicas
        * desde File/BuildSettings/PlayerSettings/InputManager
        *
        * Para que la velocidad de la nave no depena del framerate usamos Time.deltaTime. Esto funciona xq deltaTime depende 
        * del tiempo entre frames, si este el framerate es muy alto (poco tiempo entre frames) deltaTime será menor 
        * (al multiplicar por x tq x<1 el número es menor) y al contrario si el framerate es bajo deltaTime será mayor 
        */

        Vector3 thrustDirection = transform.right; // Direccion a la que apunta la nave al empezar la partida (right)
        rigid.AddForce(thrust * thrustForce * thrustDirection);
        /* transform.Rotate(transform.right * thrust * thrustForce); */
        
    
        transform.Rotate(-rotation * rotationSpeed * Vector3.forward); // Negativo para invertir rotacion

        // Para disparar

        if(Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            GameObject bullet = objectPooling.Instance.requestInstance();
            
            if(bullet != null)
            {    
                bullet.transform.position = gun.transform.position;
                bullet.transform.rotation = Quaternion.identity;
                
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.targetVector = transform.right;
                
                bullet.SetActive(true);
                
                StartCoroutine(deactivateBulet(bullet, bulletMaxLifeTime));
            }
        }
        checkOutOfBounds(); // Para que el jugador no se salga de la pantalla
    }

    private void OnCollisionEnter(Collision coliision)
    {
        if(coliision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            Debug.Log("Colision con OVNI...");
        }
    }

    private void checkOutOfBounds()
    {
        if(Mathf.Abs(transform.position.x) > 9.35)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y);
        }
        if (Mathf.Abs(transform.position.y) > 5.6)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }
    }

    // Corutina para desactivar las balas
    private IEnumerator deactivateBulet(GameObject bullet, float delay)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Desactiva la bala
        bullet.SetActive(false);
    }

}

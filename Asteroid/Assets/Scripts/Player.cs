using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Para el giro de la nave modificamos la componente trasnform
    // Para el movimiento de la nave usamos fuerzas

    public float thrustForce = 100f;  // Public permite modificar el valor desde el editor de Unity (Desde el scrript)
    public float rotationSpeed = 120f;


    public GameObject gun, bulletPrefab, meteorAtackPoint;


    private Rigidbody rigid; // Private impide modificar el valor del copmponente desde el editopr de Unity (Desde el scrript)

    void Start()
    {
        rigid = GetComponent<Rigidbody>();

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

        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = transform.right;
            /* bulletScript.speedMultiplier = thrust*thrustForce + 1; */
            /* Debug.Log(bulletScript.speedMultiplier); */
        }
    }

    private void OnCollisionEnter(Collision coliision) {
        if(coliision.gameObject.CompareTag("Enemy")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else {
            Debug.Log("Colision con OVNI...");
        }
    }
}

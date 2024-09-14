using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Para el giro de la nave modificamos la componente trasnform
    // Para el movimiento de la nave usamos fuerzas

    public float thrustForce = 200f;  // Public permite modificar el valor desde el editor de Unity (Desde el scrript)
    public float rotationSpeed = 120f;

    private Rigidbody rigid; // Private impide modificar el valor del copmponente desde el editopr de Unity (Desde el scrript)

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
        
    
        transform.Rotate(-rotation * rotationSpeed * Vector3.forward); // Negativo para invertir rotavcion
    }
}

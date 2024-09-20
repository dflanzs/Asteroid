using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> pooledOBullets, pooledMeteors, pooledMeteorChiquitos; // Usamos una lista por si necesitamos aumentar el número de elementos de la pool temporalmente

    [SerializeField] private GameObject bulletPrefab, meteorPrefab, meteorChiquitoPrefab; // Para exponer una variable en el editor de Unity sin cambiar los permisos (hacerla pública)
    
    public int poolSize;

    public Vector3 position;

    /* 
    * Para poder usar la pool desde otros scripts vamos a usar el patrón Singleton. 
    * Permite que tengamos una única instancia de la pool y que podamos acceder a los métodos desde otros scripts fácilmente
    */

    private static ObjectPooling poolInstance;

    public static ObjectPooling Instance
    {
        get { return poolInstance; }
    }

    void Awake()
    {   
        pooledOBullets = new Queue<GameObject>();
        pooledMeteors = new Queue<GameObject>();
        pooledMeteorChiquitos = new Queue<GameObject>();        

        
        if(poolInstance == null)
        {
            poolInstance = this;
        }
        else
        {
            Destroy(this);
        }   
        
        addToPool(poolSize);
    }

    private void addToPool(int amount)
    {
       // Instanciamos cada prefab y los guardamos en la poolç
        for (int i = 0; i < amount; i++)
        {
            GameObject instantiatedPrefab = Instantiate(bulletPrefab);
            instantiatedPrefab.SetActive(false);

            // Metemos los objetos a la lista
            pooledOBullets.Enqueue(instantiatedPrefab);
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject instantiatedPrefab = Instantiate(meteorPrefab);
            instantiatedPrefab.SetActive(false);

            // Metemos los objetos a la lista
            pooledMeteors.Enqueue(instantiatedPrefab);
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject instantiatedPrefab = Instantiate(meteorChiquitoPrefab);
            instantiatedPrefab.SetActive(false);

            // Metemos los objetos a la lista
            pooledMeteorChiquitos.Enqueue(instantiatedPrefab);
        }
    }
    public GameObject requestInstance(string objectType)
    {
        GameObject auxGO;
        if(objectType == "Bullet")
        {
            for (int i = 0; i < pooledOBullets.Count; i++)
            {
                if (!pooledOBullets.Peek().activeSelf) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
                {
                    auxGO = pooledOBullets.Dequeue();
                    pooledOBullets.Enqueue(auxGO);
                    return auxGO;
                }
            }
            return null;
        } 
        else if (objectType == "Meteor")
        {
            for (int i = 0; i < pooledOBullets.Count; i++)
            {
                if (!pooledMeteors.Peek().activeSelf) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
                {
                    auxGO = pooledMeteors.Dequeue();
                    pooledMeteors.Enqueue(auxGO);
                    return auxGO;
                }
            }
            return null;
        } 
        else if (objectType == "MeteorChiquito")
        {
            for (int i = 0; i < pooledMeteorChiquitos.Count; i++)
            {
                if (!pooledMeteorChiquitos.Peek().activeSelf) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
                {
                    auxGO = pooledMeteorChiquitos.Dequeue();
                    pooledMeteorChiquitos.Enqueue(auxGO);
                    return auxGO;
                }
            }
            return null;
        } 
        else
        {
            return null;
        }
    }
}

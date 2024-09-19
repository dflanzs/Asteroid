using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledOBullets; // Usamos una lista por si necesitamos aumentar el número de elementos de la pool temporalmente
    
    [SerializeField] private List<GameObject> pooledOMeteors; // Usamos una lista por si necesitamos aumentar el número de elementos de la pool temporalmente

    [SerializeField] private GameObject prefabToPool; // Para exponer una variable en el editor de Unity sin cambiar los permisos (hacerla pública)
    
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
            GameObject instantiatedPrefab = Instantiate(prefabToPool);
            instantiatedPrefab.SetActive(false);

            // Metemos los objetos a la lista
            pooledOBullets.Add(instantiatedPrefab);
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToPool);
            instantiatedPrefab.SetActive(false);

            // Metemos los objetos a la lista
            pooledOMeteors.Add(instantiatedPrefab);
        }
    }

    public GameObject requestInstance(string objectType)
    {
        if(objectType == "Bullet")
        {
            for (int i = 0; i < pooledOBullets.Count; i++)
            {
                if (!pooledOBullets[i].activeInHierarchy) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
                {
                    return pooledOBullets[i];
                }
            }
            // Si todos los objetos están ocupados creamos uno nuevo al final de la lista y lo devolvemos
            addToPool(1);
            pooledOBullets[pooledOBullets.Count - 1].SetActive(false);
            poolSize++;

            return pooledOBullets[pooledOBullets.Count - 1];
        } 
        else if (objectType == "Meteor")
        {
            for (int i = 0; i < pooledOBullets.Count; i++)
            {
                if (!pooledOMeteors[i].activeInHierarchy) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
                {
                    return pooledOMeteors[i];
                }
            }
            // Si todos los objetos están ocupados creamos uno nuevo al final de la lista y lo devolvemos
            addToPool(1);
            pooledOMeteors[pooledOMeteors.Count - 1].SetActive(false);
            poolSize++;

            return pooledOMeteors[pooledOMeteors.Count - 1];
        } 
        else
        {
            return null;
        }
    }
}

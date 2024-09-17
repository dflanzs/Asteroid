using System.Collections.Generic;
using UnityEngine;


public class objectPooling : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects; // Usamos una lista por si necesitamos aumentar el número de elementos de la pool temporalmente
    
    [SerializeField] private GameObject prefabToPool; // Para exponer una variable en el editor de Unity sin cambiar los permisos (hacerla pública)
    
    public int poolSize;

    /* 
    * Para poder usar la pool desde otros scripts vamos a usar el patrón Singleton. 
    * Permite que tengamos una única instancia de la pool y que podamos acceder a los métodos desde otros scripts fácilmente
    */

    private static objectPooling poolInstance;

    public static objectPooling Instance
    {
        get { return poolInstance; }
    }

    void Awake()
    {   
        poolInstance = this;   
        
        addToPool(poolSize);
    }

    private void addToPool(int amount)
    {
       // Instanciamos cada prefab y los guardamos en la pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instantiatedPrefab = Instantiate(prefabToPool);
            instantiatedPrefab.SetActive(false);

            // Para prdenar indicamos que el padre de los objetos de la pool es la propia pool
            instantiatedPrefab.transform.SetParent(transform); 
        }
    }

    public GameObject requestInstance()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) // Comprobamos si el elemento está inactivo para saber si podemos utilizarlo
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null; // Si todos los objetos están ocupados
    }
}

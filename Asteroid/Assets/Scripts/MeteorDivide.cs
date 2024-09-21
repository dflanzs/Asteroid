using UnityEngine;

public class MeteorDivide : MonoBehaviour
{
    public GameObject meteorChiquitoPrefab1, meteorChiquitoPrefab2;

    public int speedMultiplier;
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            meteorChiquitoPrefab1 = ObjectPooling.Instance.requestInstance("MeteorChiquito");
            meteorChiquitoPrefab2 = ObjectPooling.Instance.requestInstance("MeteorChiquito");
            
            if(meteorChiquitoPrefab1 != null && meteorChiquitoPrefab2 != null)
            {
                meteorChiquitoPrefab1.SetActive(true);
                meteorChiquitoPrefab2.SetActive(true);

                meteorChiquitoPrefab1.transform.position = transform.position;
                meteorChiquitoPrefab2.transform.position = transform.position + new Vector3(-1, 0, 0); //Separamos para evitar bugs

                meteorChiquitoPrefab1.GetComponent<Rigidbody>().AddForce(new Vector3(1, -1, 0) * speedMultiplier);
                meteorChiquitoPrefab2.GetComponent<Rigidbody>().AddForce(new Vector3(-1, -1, 0) * speedMultiplier);
            }
        }
    }
}

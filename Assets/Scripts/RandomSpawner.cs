using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ItemPrefab; //public GameObject ItemPrefab;
    [SerializeField] private float radius = 1; //public float radius = 1;
    [SerializeField] private float totalSheep = 3; 
    private float counter = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (counter < totalSheep){
            Invoke("SpawnObjectAtRandom", 3.0F); 
        }
        //Invoke("SpawnObjectAtRandom", 3.0F); 
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     SpawnObjectAtRandom();
        // }
    }
    

    void SpawnObjectAtRandom(){
        Vector3 randomPos = Random.insideUnitCircle * radius;
        Instantiate(ItemPrefab, this.transform.position + randomPos, Quaternion.identity); //Instantiate(ItemPrefab, this.transform.position + randomPos, Quaternion.identity);
        CancelInvoke();
        counter = counter + 1;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}

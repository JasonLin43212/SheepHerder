using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject WhiteSheep; //public GameObject ItemPrefab;
    [SerializeField] private GameObject BlackSheep; // black sheep
    [SerializeField] private float radius;
    [SerializeField] private float totalSheep; 
    private float counter = 0; 

    // Update is called once per frame
    void Update()
    {   
        if (counter < totalSheep){
            Invoke("SpawnObjectAtRandom", 3); 
        }        
    }


    void SpawnObjectAtRandom(){
        Vector2 circleRandomPos = Random.insideUnitCircle * radius;
        Vector3 randomPos = new Vector3(circleRandomPos.x, circleRandomPos.y, -1); // -1 to make sheep visible
        var val = Random.value;
        if (val < 0.5f){
            Instantiate(WhiteSheep, this.transform.position + randomPos, Quaternion.identity); 
        } else {
            Instantiate(BlackSheep, this.transform.position + randomPos, Quaternion.identity); 
        }
        //Instantiate(ItemPrefab, this.transform.position + randomPos, Quaternion.identity); //Instantiate(ItemPrefab, this.transform.position + randomPos, Quaternion.identity);
        CancelInvoke();
        counter = counter + 1;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

    
}

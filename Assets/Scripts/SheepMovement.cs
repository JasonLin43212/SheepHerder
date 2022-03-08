using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private float directionChangeTime = 3f;
    private float characterVelocity = 0.5f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;


    void Start()
    {
        latestDirectionChangeTime = 0f;
        directionChangeTime = Random.Range(3f, 7f);
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
            directionChangeTime = Random.Range(3f, 7f);
        }

        //move enemy: 
        Vector3 temp_position = new Vector3(
            transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime),
            transform.position.z
        );

    /*    //upper left square
        if (temp_position.x > -16 && temp_position.x < -10 && temp_position.y < 9 && temp_position.y > 3)
        {
            return;
        }
        //lower left square
        else if (temp_position.x > -16 && temp_position.x < -10 && temp_position.y < -3 && temp_position.y > -9){
            return;
        }
        //lower right 
        else if (temp_position.x > 9 && temp_position.x < 15 && temp_position.y < -3 && temp_position.y > -9)
        {
            return;
        }
        //upper right
        else if (temp_position.x > 9 && temp_position.x < 15 && temp_position.y < 9 && temp_position.y > 3)
        {
            return;
        }       
        //only update if not in a pen
        else{*/
            transform.position = temp_position;
        /*}*/


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            movementDirection *= -1;
        }
    }
}

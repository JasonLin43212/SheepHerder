using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private float latestDirectionChangeTime;
    private float directionChangeTime = 3f;
    private float characterSpeed = 0.5f;
    private float runSpeed = 2.0f;
    private Vector2 currentVelocity; 
    private bool pushed;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        directionChangeTime = Random.Range(3f, 7f);
        calcuateNewMovementVector();
        
    }

    void calcuateNewMovementVector()
    {
        latestDirectionChangeTime = Time.time;
        directionChangeTime = Random.Range(3f, 7f);
        pushed = false;
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        currentVelocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * characterSpeed;
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            calcuateNewMovementVector();
        } else {
            if (!pushed) {
                body.velocity = currentVelocity;
            }
        }


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
        /*}*/


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            calcuateNewMovementVector();
        }
    }

    public void runAway(Vector2 dogLocation) {
        pushed = false;
        Vector2 runDirection = new Vector2(transform.position.x, transform.position.y) - dogLocation;
        currentVelocity = runDirection.normalized * runSpeed;
        latestDirectionChangeTime = Time.time;
        directionChangeTime = Random.Range(4f, 6f);
    }

    public void push(Vector2 dogVelocity) {
        pushed = true;
        latestDirectionChangeTime = Time.time;
        directionChangeTime = 1f;
        body.velocity = dogVelocity * 0.95f;
    }
}

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
    private bool biten;
    private CircleCollider2D sheepCollider;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sheepCollider = GetComponent<CircleCollider2D>();
        latestDirectionChangeTime = 0f;
        directionChangeTime = Random.Range(3f, 7f);
        calcuateNewMovementVector();
        
    }

    void calcuateNewMovementVector()
    {
        latestDirectionChangeTime = Time.time;
        directionChangeTime = Random.Range(3f, 7f);
        
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        currentVelocity = inPen()
            ? Vector2.zero
            : new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * characterSpeed;
    }

    void Update()
    {
        if (biten) {
            return;
        }
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            calcuateNewMovementVector();
        } else {
            body.velocity = currentVelocity;
        }

    }
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Wall")
        {
            calcuateNewMovementVector();
        }
    }

    public void runAway(Vector2 dogLocation) {
        Vector2 runDirection = new Vector2(transform.position.x, transform.position.y) - dogLocation;
        currentVelocity = runDirection.normalized * runSpeed;
        body.velocity = currentVelocity;
        latestDirectionChangeTime = Time.time;
        directionChangeTime = Random.Range(2f, 4f);
    }

    // Returns whether you can bite it or not
    public bool bite() {
        if (biten) { return false; }
        else {
            biten = true;
            return true;
        }
    }

    public void unBite() {
        biten = false;
    }

    public void drag(Vector2 velocity) {
        body.velocity = velocity;
    }

    private bool inPen() {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, sheepCollider.radius);
        foreach (Collider2D overlap in overlaps) {
            if (overlap.gameObject.tag == "Pen") {
                return true;
            }
        }
        return false;
    }

}

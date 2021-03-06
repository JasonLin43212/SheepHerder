using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    [SerializeField] private GameObject sheepBar;
    private Sheepbar myBar;
    private Rigidbody2D body;
    private float latestDirectionChangeTime;
    private float directionChangeTime = 3f;
    private float characterSpeed = 0.5f;
    private float runSpeed = 3.0f;
    private Vector2 currentVelocity; 
    private bool biten;
    private CircleCollider2D sheepCollider;
    private float inPenFor;
    private float disappearThreshold = 15; //in seconds
    private float inPenStartTime;
    private bool wasInPen;
    private float transformNum = 0.5f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sheepCollider = GetComponent<CircleCollider2D>();
        latestDirectionChangeTime = 0f;
        directionChangeTime = Random.Range(3f, 7f);
        calcuateNewMovementVector();
        inPenFor = 0;

        myBar = sheepBar.GetComponent<Sheepbar>();
        myBar.setParams(this.gameObject, disappearThreshold);
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

    void FixedUpdate() {
        if (PlayersScore.pause) {
            return;
        }
        if (inPen()) {
            if (!wasInPen) {
                wasInPen = true;
                inPenStartTime = Time.time;
            }
            myBar.shouldShow(true);
            inPenFor = Time.time - inPenStartTime;
        } else {
            inPenFor = 0;
            wasInPen = false;
            myBar.shouldShow(false);
        }
    }

    void Update()
    {
        if (PlayersScore.pause) {
            return;
        }

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

        if (!biten) {
            if (body.velocity.x > 0) {
                transform.localScale = Vector3.one * transformNum;
            } else if (body.velocity.x < 0) {
                transform.localScale = new Vector3(-transformNum, transformNum, transformNum);
            }
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
        Vector2 randomOffset = new Vector2(
            Random.Range(-0.5f, 0.5f), 
            Random.Range(-0.5f, 0.5f)
        );
        currentVelocity = (runDirection + randomOffset).normalized * runSpeed;
        body.velocity = currentVelocity;
        latestDirectionChangeTime = Time.time;
        directionChangeTime = Random.Range(1f, 3f);
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
        calcuateNewMovementVector();
    }

    public void drag(Vector2 position, Vector2 velocity) {
        body.position = new Vector3(position.x, position.y);
        body.velocity = new Vector2(velocity.x, velocity.y);
    }

    private bool inPen() {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(sheepCollider.bounds.center, sheepCollider.radius * transformNum);
        foreach (Collider2D overlap in overlaps) {
            if (overlap.gameObject.tag == "Pen") {
                return true;
            }
        }
        return false;
    }

    public bool readyToDisappear() {
        return inPenFor >= disappearThreshold;
    }

    public float getCurrentTimeInPen() {
        return inPenFor;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private System.String playerNumber;
    [SerializeField] private GameObject thisBark;
    private Rigidbody2D body;
    private Vector2 prevDirection;
    private float dashSpeed = 20;
    private float speed = 8;
    private float barkRadius = 3;
    private bool isDashing;
    private int currentDashingCooldown;
    private int dashingCooldown = 15;
    private Collider2D dogCollider;

    private float transformNum = 0.75f;
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dogCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode dashKeyCode = playerNumber == "1"
            ? KeyCode.BackQuote
            : KeyCode.Comma;

        KeyCode barkKeyCode = playerNumber == "1"
            ? KeyCode.Alpha1
            : KeyCode.Period;
        
        if (isDashing) {
            body.velocity = prevDirection * dashSpeed;
            currentDashingCooldown--;
            if (currentDashingCooldown <= 0) {
                isDashing = false;
            }
        } else if (Input.GetKeyDown(dashKeyCode)) {
            isDashing = true;
            currentDashingCooldown = 15;
        } else {
            float horizontalInput = Input.GetAxis("Horizontal" + playerNumber);
            float verticalInput = Input.GetAxis("Vertical" + playerNumber);

            if (horizontalInput > 0) {
                transform.localScale = Vector3.one * transformNum;
            } else if (horizontalInput < 0) {
                transform.localScale = new Vector3(-transformNum, transformNum, transformNum);
            }
            
            body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
            prevDirection = body.velocity.normalized;
        }

        if (Input.GetKeyDown(barkKeyCode)) {
            bark();
        }
    }

    private void bark() {
        Collider2D[] affectedByBark = Physics2D.OverlapCircleAll(dogCollider.bounds.center, barkRadius);
        foreach (Collider2D affectedCollider in affectedByBark) {
            GameObject affectedObject = affectedCollider.gameObject;
            if (affectedObject.layer == 7) { // 7 is the sheep layer
                affectedObject.GetComponent<SheepMovement>().runAway(new Vector2(transform.position.x, transform.position.y));
            }
        }
        GameObject currentBark = Instantiate(thisBark);
        currentBark.GetComponent<Bark>().setParams(this.gameObject, barkRadius);

    }

    private void OnCollisionStay2D(Collision2D collisionInfo)
    {
        GameObject collisionObject = collisionInfo.gameObject;
        if (collisionObject.layer == 7) {
            collisionObject.GetComponent<SheepMovement>().push(body.velocity);
        }
    }
}

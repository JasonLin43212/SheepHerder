using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private System.String playerNumber;
    private Rigidbody2D body;
    private Vector2 prevVelocity;
    private float dashSpeed = 20;
    private float speed = 4;
    private bool isDashing;
    private int dashingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode dashKeyCode = playerNumber == "1"
            ? KeyCode.BackQuote
            : KeyCode.Comma;
        
        if (isDashing) {
            body.velocity = prevVelocity * dashSpeed;
            dashingCooldown--;
            if (dashingCooldown <= 0) {
                isDashing = false;
            }
        } else if (Input.GetKeyDown(dashKeyCode)) {
            isDashing = true;
            dashingCooldown = 15;
        } else {
            float horizontalInput = Input.GetAxis("Horizontal" + playerNumber);
            float verticalInput = Input.GetAxis("Vertical" + playerNumber);
            body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
            prevVelocity = body.velocity.normalized;
        }
    }
}

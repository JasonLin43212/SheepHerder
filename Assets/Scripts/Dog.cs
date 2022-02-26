using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private System.String playerNumber;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal" + playerNumber);
        float verticalInput = Input.GetAxis("Vertical" + playerNumber);

        body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
    }
}

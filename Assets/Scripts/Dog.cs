using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dog : MonoBehaviour
{
    [SerializeField] private System.String playerNumber;
    [SerializeField] private GameObject thisBark;
    [SerializeField] private GameObject biteArea;
    private Rigidbody2D body;
    private Vector2 prevDirection;
    private Bite myBite;
    private float dashSpeed = 20;
    private float speed = 8;
    private float barkRadius = 3;
    private bool isDashing;
    private int currentDashingCooldown;
    private int dashingCooldown = 15;
    private BoxCollider2D dogCollider;

    private float transformNum = 0.75f;

    private List<GameObject> bittenSheep = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dogCollider = GetComponent<BoxCollider2D>();

        // instantiate bite indicator
        GameObject currentBite = Instantiate(biteArea);
        myBite = currentBite.GetComponent<Bite>();
        myBite.setParams(this.gameObject, getBiteSize());
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

        KeyCode biteKeyCode = playerNumber == "1"
            ? KeyCode.Alpha2
            : KeyCode.Slash;
        
        if (isDashing) {
            body.velocity = prevDirection * dashSpeed;
            currentDashingCooldown--;
            if (currentDashingCooldown <= 0) {
                isDashing = false;
            }
        } else if (Input.GetKeyDown(dashKeyCode)) {
            isDashing = true;
            currentDashingCooldown = dashingCooldown;
        } else {
            float horizontalInput = Input.GetAxis("Horizontal" + playerNumber);
            float verticalInput = Input.GetAxis("Vertical" + playerNumber);

            if (bittenSheep.Count == 0) { // When you are draggin sheep, don't change orientation of dog
                if (horizontalInput > 0) {
                    transform.localScale = Vector3.one * transformNum;
                } else if (horizontalInput < 0) {
                    transform.localScale = new Vector3(-transformNum, transformNum, transformNum);
                }
            }
            
            body.velocity = new Vector2(horizontalInput * speed, verticalInput * speed);
            prevDirection = body.velocity.normalized;
        }

        if (Input.GetKeyDown(biteKeyCode)) {
            bite();
            myBite.setBite(true);
        } else if (Input.GetKeyUp(biteKeyCode)) {
            myBite.setBite(false);
            for (int i = 0; i < bittenSheep.Count; i++) {
                bittenSheep[i].GetComponent<SheepMovement>().unBite();
            }
            bittenSheep.Clear();
        }

        if (bittenSheep.Count > 0) {
            Collider2D[] affectedByBite = Physics2D.OverlapBoxAll(getBiteCenter(), getBiteSize(), 0);
            List<Collider2D> affectedByBiteList = new List<Collider2D>(affectedByBite);

            // Check that the bitten sheep is still within range
            foreach (GameObject bittenSheepObject in bittenSheep) {
                if (affectedByBiteList.Contains(bittenSheepObject.GetComponent<Collider2D>())) {
                    bittenSheepObject.GetComponent<SheepMovement>().drag(body.velocity);
                } else {
                    bittenSheepObject.GetComponent<SheepMovement>().drag(Vector2.zero);
                }
            }
        }

        if (Input.GetKeyDown(barkKeyCode)) {
            bark();
        }
    }

    private void bite() {
        // Dog can only bite in the direction that they are facing
        Collider2D[] affectedByBite = Physics2D.OverlapBoxAll(getBiteCenter(), getBiteSize(), 0);

        foreach (Collider2D bittenCollider in affectedByBite) {
            GameObject bittenObject = bittenCollider.gameObject;
            if (bittenObject.layer == 7) { // is a sheep
                bool canBite = bittenObject.GetComponent<SheepMovement>().bite();
                if (canBite) {
                    bittenSheep.Add(bittenObject);
                }
            } else if (bittenObject.layer == 8) { // is a dog
                bittenObject.GetComponent<Dog>();
            }

        }
    }

    public Vector2 getBiteCenter() {
        Bounds dogBounds = dogCollider.bounds;
        bool isFacingRight = transform.localScale.x > 0;
        return new Vector2(
            isFacingRight ? dogBounds.max.x : dogBounds.min.x,
            dogBounds.center.y
        );
    }

    public Vector2 getBiteSize() {
        return new Vector2(
            1f,
            dogCollider.bounds.size.y
        );
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

}

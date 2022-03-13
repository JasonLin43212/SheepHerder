using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dog : MonoBehaviour
{
    [SerializeField] private System.String playerNumber;
    [SerializeField] private GameObject thisBark;
    [SerializeField] private GameObject biteArea;
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject barkBar;
    private Rigidbody2D body;
    private Vector2 prevDirection;
    private Bite myBite;
    private float dashSpeed = 20;
    private float speed = 8;
    private float homeSpeed = 0.15f;
    private float barkRadius = 5;
    private bool isDashing;
    private int currentDashingCooldown;
    private int dashingCooldown = 15;
    private BoxCollider2D dogCollider;

    private float transformNum = 0.75f;

    private List<GameObject> bittenSheep = new List<GameObject>();
    private List<Vector2> relativePos = new List<Vector2>();
    private bool isGoingHome = false;
    private int barkHoldDown = 40;
    private int currentBarkHoldDown = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        dogCollider = GetComponent<BoxCollider2D>();

        // instantiate bite indicator
        GameObject currentBite = Instantiate(biteArea);
        myBite = currentBite.GetComponent<Bite>();
        myBite.setParams(this.gameObject, getBiteSize());

        barkBar.GetComponent<BarkBar>().setParams(this.gameObject, barkHoldDown);
        barkBar.GetComponent<BarkBar>().shouldShow(false);
    }

    void FixedUpdate()
    {
        if (isGoingHome) {
            BoxCollider2D homeCollider = home.GetComponent<BoxCollider2D>();
            Vector3 homeCenter3 = homeCollider.bounds.center;
            Vector2 homeCenter2 = new Vector2(homeCenter3.x, homeCenter3.y);
            Vector2 toHomeDirection = (homeCenter2 - body.position).normalized;

            // check if the dog is back at home
            Vector2 homeSize = new Vector2(homeCollider.bounds.size.x, homeCollider.bounds.size.y); 
            Collider2D[] inHome = Physics2D.OverlapBoxAll(homeCenter2, homeSize, 0);
            foreach (Collider2D colliderInHome in inHome) {
                if (colliderInHome.gameObject == this.gameObject) {
                    GetComponent<SpriteRenderer>().color = Color.white;
                    isGoingHome = false;
                    return;
                }
            }

            body.velocity = Vector2.zero;
            body.position += toHomeDirection * homeSpeed + new Vector2(0, Random.Range(-0.3f, 0.3f));

            if (toHomeDirection.x > 0) {
                transform.localScale = Vector3.one * transformNum;
            } else if (toHomeDirection.x < 0) {
                transform.localScale = new Vector3(-transformNum, transformNum, transformNum);
            }

            GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 0.5f);

            return;
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoingHome) {
            return;
        }

        KeyCode dashKeyCode = playerNumber == "1"
            ? KeyCode.BackQuote
            : KeyCode.Comma;

        KeyCode barkKeyCode = playerNumber == "1"
            ? KeyCode.Alpha1
            : KeyCode.Period;

        KeyCode biteKeyCode = playerNumber == "1"
            ? KeyCode.Alpha2
            : KeyCode.Slash;
        
        // - You can dash
        // - You cannot bark while dashing
        // - You cannot initiate a dash while holding a bark
        // - You cannot move while doing any of the above things
        if (isDashing) {
            body.velocity = prevDirection * dashSpeed;
            currentDashingCooldown--;
            if (currentDashingCooldown <= 0) {
                isDashing = false;
            }
        } else if (Input.GetKey(barkKeyCode)) {
            body.velocity = Vector2.zero;
            barkBar.GetComponent<BarkBar>().shouldShow(true);
            if (currentBarkHoldDown < barkHoldDown) {
                currentBarkHoldDown++;
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
            stopBite();
            myBite.setSuccessBite(false);
        }

        if (bittenSheep.Count > 0) {
            Collider2D[] affectedByBite = Physics2D.OverlapBoxAll(getBiteCenter(), getBiteSize(), 0);
            List<Collider2D> affectedByBiteList = new List<Collider2D>(affectedByBite);

            // Check that the bitten sheep is still within range
            for (int i = 0; i < bittenSheep.Count; i++) {
                GameObject bittenSheepObject = bittenSheep[i];
                if (bittenSheepObject == null) {
                    continue;
                }
                // Stop sheep from being given a velocity if they aren't within biting distance
                if (affectedByBiteList.Contains(bittenSheepObject.GetComponent<Collider2D>())) {
                    bittenSheepObject.GetComponent<SheepMovement>().drag(body.position + relativePos[i], body.velocity);
                } else {
                    bittenSheepObject.GetComponent<SheepMovement>().drag(body.position + relativePos[i], Vector2.zero);
                }
            }
        }

        if (Input.GetKeyUp(barkKeyCode)) {
            if (currentBarkHoldDown == barkHoldDown) {
                bark();
            }
            currentBarkHoldDown = 0;
            barkBar.GetComponent<BarkBar>().shouldShow(false);
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
                    myBite.setSuccessBite(true);
                    bittenSheep.Add(bittenObject);
                    relativePos.Add(new Vector2(
                        bittenObject.transform.position.x - body.position.x,
                        bittenObject.transform.position.y - body.position.y
                    ));
                }
            } else if (bittenObject.layer == 8) { // is a dog
                if (bittenObject != this.gameObject) {
                    bittenObject.GetComponent<Dog>().goHome();
                }
            }

        }
    }

    private void stopBite() {
        myBite.setBite(false);
        for (int i = 0; i < bittenSheep.Count; i++) {
            if (bittenSheep[i] != null) {
                bittenSheep[i].GetComponent<SheepMovement>().unBite();
            }
        }
        bittenSheep.Clear();
        relativePos.Clear();
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
            1.2f,
            dogCollider.bounds.size.y * 0.8f
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

    public void goHome() {
        isGoingHome = true;
        stopBite();
        currentBarkHoldDown = 0;
        barkBar.GetComponent<BarkBar>().shouldShow(false);
    }

    public int getCurrentBarkHoldDown() {
        return currentBarkHoldDown;
    }
}

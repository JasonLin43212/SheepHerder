using UnityEngine;

public class Bark : MonoBehaviour
{
    private float barkRadius = 0;
    private float currentRadius = 0;
    private GameObject dog;

    public void setParams(GameObject dog_, float barkRadius_) {
        dog = dog_;
        barkRadius = barkRadius_ * 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(dog.transform.position.x, dog.transform.position.y, -1);
        transform.localScale = Vector3.one * currentRadius;
        currentRadius += 0.2f;
        if (currentRadius > barkRadius) {
            Destroy(this.gameObject);
        }
    }
}

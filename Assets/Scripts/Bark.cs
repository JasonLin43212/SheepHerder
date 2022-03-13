using UnityEngine;

public class Bark : MonoBehaviour
{
    private int duration = 20;
    private int currentTimeAlive = 0;

    public void setParams(GameObject dog_, float barkRadius_) {
        transform.position = new Vector3(dog_.transform.position.x, dog_.transform.position.y, -1);
        transform.localScale = Vector3.one * barkRadius_ * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeAlive >= duration) {
            Destroy(this.gameObject);
        } else {
            currentTimeAlive++;
        }
    }
}

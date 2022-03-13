using UnityEngine;

public class Bite : MonoBehaviour
{
    private GameObject dog;
    private Vector2 size;
    private bool showBite;

    public void setParams(GameObject dog_, Vector2 size_) {
        dog = dog_;
        size = size_;
        showBite = false;
    }

    public void setBite(bool showBite_) {
        showBite = showBite_;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 biteCenter = dog.GetComponent<Dog>().getBiteCenter();
        transform.position = new Vector3(
            biteCenter.x, 
            biteCenter.y, 
            showBite ? -1.5f : 2f
        );
        transform.localScale = new Vector3(size.x, size.y, 1);
    }
}

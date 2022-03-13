using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkBar : MonoBehaviour
{
    private GameObject dog;
    private int maxHoldDown;
    private bool show;

    public void setParams(GameObject dog_, int maxHoldDown_) {
        dog = dog_;
        maxHoldDown = maxHoldDown_;
        show = false;
    }

    public void shouldShow(bool show_) {
        show = show_;
    }
    void Update()
    {
        float currentHoldDown = (float) dog.GetComponent<Dog>().getCurrentBarkHoldDown();
        float progress = currentHoldDown / maxHoldDown;
        GetComponent<SpriteRenderer>().color = show
            ? progress == 1
                ? new Color(0, 0, 1, 0.75f)
                : new Color(1 - progress, progress, 0, 0.75f)
            : Color.clear;
        transform.localScale = new Vector3(
            progress * 3f,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}

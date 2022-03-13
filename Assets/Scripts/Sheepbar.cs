using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheepbar : MonoBehaviour
{
    private GameObject sheep;
    private int maxDuration;
    private bool show;

    void Start() {
        show = false;
    }
    public void setParams(GameObject sheep_, int maxDuration_) {
        sheep = sheep_;
        maxDuration = maxDuration_;
        show = false;
    }

    public void shouldShow(bool show_) {
        show = show_;
    }
    void Update()
    {
        float currentHoldDown = (float) sheep.GetComponent<SheepMovement>().getCurrentTimeInPen();
        float progress = currentHoldDown / maxDuration;
        GetComponent<SpriteRenderer>().color = show
            ? new Color(1 - progress, progress, 0, 0.75f)
            : Color.clear;
        transform.localScale = new Vector3(
            progress * 3f,
            transform.localScale.y,
            transform.localScale.z
        );
    }
}

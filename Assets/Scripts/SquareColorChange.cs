using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareColorChange : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        m_SpriteRenderer.color = Color.black;
    }

    void Update()
    {
     
    } 
}

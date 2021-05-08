using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is responsible for visually sorting the depth of a gameObject's sprite, by using its y position.
*/
public class LayerSorter : MonoBehaviour
{
    private SpriteRenderer sprRenderer; //to access & change sprite renderer
    public int sorter; //variable determines sprite's order in layer
    private float ypos;
    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        sorter = (int)-(ypos * 100);
        sprRenderer.sortingOrder = sorter;
    }

    void FixedUpdate()
    {
        ypos = transform.position.y;
        sorter = (int)-(ypos * 100);
        sprRenderer.sortingOrder = sorter;
    }
}

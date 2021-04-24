﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasing : MonoBehaviour
{
    public float ray_distance;
    public static Animator animator;
    public float speed;
    public GameObject player;
    private SpriteRenderer mySpriteRenderer;
    public static int move_vertical;
    public static int move_horizontal;
   
    public LayerMask barrier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, ray_distance, barrier);
        Vector3 localPosition = player.transform.position - transform.position;

        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * speed * move_vertical, move_horizontal * localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
        if (hit.collider != null)
        {

            move_vertical = 0;
            move_horizontal = 2;
          

        }
        else
        {
            move_vertical = 1;
            move_horizontal = 1;
        }

        if (player.transform.position.x < transform.position.x)
        {
            mySpriteRenderer.flipX = false;
        }
        else
        {
            mySpriteRenderer.flipX = true;
        }
       


    }
}

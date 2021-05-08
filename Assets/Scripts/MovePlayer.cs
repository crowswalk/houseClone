﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This Script is responsible for moving the player

public class MovePlayer : MonoBehaviour
{
    public bool canMove;
    public float speed;
    public Vector2 dir;

    private SpriteRenderer sprRenderer; //to access & change sprite renderer
    private Sprite[] walkingSprites; //which sprite is currently being displayed
    private Sprite currentSprite; //currently displayed sprite
    private Sprite stillSprite; //sprite to show when there is no movement
    public float framerate; //frames per second for walking animation
    private float currentFrame; //currently displayed frame

    private BoxCollider2D playerCollider;
    private Inventory inventory;

    [SerializeField]
    public Sprite[] normalSprites = new Sprite[1]; //normal walking sprite without items
    public Sprite[] axeSprites = new Sprite[1];
    public Sprite[] keySprites = new Sprite[1];
    public Sprite[] plungeSprites = new Sprite[1];
    public Sprite[] ballSprites = new Sprite[1];
    public Sprite[] gunSprites = new Sprite[1];
    public Sprite[] trapSprites = new Sprite[1];

    void Start()
    {
        walkingSprites = normalSprites;
        stillSprite = walkingSprites[0];
        currentFrame = 0;
        sprRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        inventory = GetComponent<Inventory>();
    }

    void FixedUpdate()
    {

         if(!canMove){
             sprRenderer.sprite = stillSprite;
            return;
        }
        sprRenderer.sprite = currentSprite; //show sprite that was calculated in walkCycle
        checkKey(); //check key input
    }

   public void changeSprites(string currentItem) {
        if (currentItem.Contains("BowlingBall")) {
            walkingSprites = ballSprites;
        } else if (currentItem.Contains("Axe")) {
            walkingSprites = axeSprites;
        } else if (currentItem.Contains("BearTrap")) {
            walkingSprites = trapSprites;
        } else if (currentItem.Contains("Key")) {
            walkingSprites = keySprites;
        } else if (currentItem.Contains("Shotgun")) {
            walkingSprites = gunSprites;
        } else if (currentItem.Contains("Plunger")) {
            walkingSprites = plungeSprites;
        } else {
            walkingSprites = normalSprites;
        }
    }

    void walkCycle()
    {
        stillSprite = walkingSprites[0];
        if ((int)currentFrame < walkingSprites.Length)
        {
            currentSprite = walkingSprites[(int)currentFrame]; //change sprite
        }
        else
        {
            currentFrame = 0;
        }
        currentFrame += Time.deltaTime * framerate; //add frames per second
    }

    void checkKey()
    { //Check input & move player
        if (Input.GetKey(KeyCode.W))
        { //UP 
            walkCycle();
            if (Input.GetKey(KeyCode.A))
            { //UP & LEFT
                transform.Translate(new Vector3(-.75f, .75f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(-1, 1);
            }
            else if (Input.GetKey(KeyCode.D))
            { //UP & RIGHT
                transform.Translate(new Vector3(.75f, .75f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(1, 1);
            }
            else
            { //ONLY UP
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                dir = new Vector2(0, 1);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        { //DOWN
            walkCycle();
            if (Input.GetKey(KeyCode.A))
            { //DOWN & LEFT
                transform.Translate(new Vector3(-.75f, -.75f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(-1, -1);
            }
            else if (Input.GetKey(KeyCode.D))
            { //DOWN & RIGHT
                transform.Translate(new Vector3(.75f, -.75f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(1, -1);
            }
            else
            { //ONLY DOWN
                transform.Translate(Vector3.down * Time.deltaTime * speed);
                dir = new Vector2(0, -1);
            }
        }
        else
        { //NOT UP OR DOWN
            if (Input.GetKey(KeyCode.A))
            { //LEFT
                walkCycle();
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                dir = new Vector2(-1, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            { //RIGHT
                walkCycle();
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                dir = new Vector2(1, 0);
            }
            else
            {
                currentSprite = stillSprite;
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            sprRenderer.flipX = true;
        } else if (Input.GetKey(KeyCode.D)) {
            sprRenderer.flipX = false;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This Script is responsible for moving the player

public class MovePlayer : MonoBehaviour
{
    public float speed;
    private BoxCollider2D playerCollider;

    public Vector2 dir = new Vector2(0,-1);

    void Start() {
        playerCollider = GetComponent < BoxCollider2D > ();
    }

    void FixedUpdate() {
        checkKey(); //check key input
    }

    void checkKey() { //Check input & move player
        if (Input.GetKey(KeyCode.W)) { //UP  
            if(Input.GetKey(KeyCode.A)) { //UP & LEFT
                transform.Translate(new Vector3(-.5f, .5f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(-1,1);
            } else if (Input.GetKey(KeyCode.D)) { //UP & RIGHT
                transform.Translate(new Vector3(.5f, .5f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(1, 1);
            } else { //ONLY UP
                transform.Translate(Vector3.up * Time.deltaTime * speed);
                dir = new Vector2(0, 1);
            }
        } else if (Input.GetKey(KeyCode.S)) { //DOWN
            if(Input.GetKey(KeyCode.A)) { //DOWN & LEFT
                transform.Translate(new Vector3(-.5f, -.5f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(-1, -1);
            } else if (Input.GetKey(KeyCode.D)) { //DOWN & RIGHT
                transform.Translate(new Vector3(.5f, -.5f, 0f) * Time.deltaTime * speed);
                dir = new Vector2(1, -1);
            } else { //ONLY DOWN
                transform.Translate(Vector3.down * Time.deltaTime * speed);
                dir = new Vector2(0, -1);
            }
        } else { //NOT UP OR DOWN
            if (Input.GetKey(KeyCode.A)) { //LEFT
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                dir = new Vector2(-1, 0);
            } else if (Input.GetKey(KeyCode.D)) { //RIGHT
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                dir = new Vector2(1, 0);
            }
        }

    }

}

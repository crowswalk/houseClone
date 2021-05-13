using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script is responsible for detecting triggers that the player collides with.
*/
public class PlayerTriggers : MonoBehaviour
{
    public CamFollow camera;
    Camera mainCam;
    public static bool sister_follow;
    public GameObject sister;
    public GameObject dad;
    private bool dadenter;
    private Vector3 daddest;

    public SoundManager sound;
    void Start() {
        mainCam = Camera.main;
        daddest = gameObject.transform.position;
    }

     void Update()
    {
        //Debug.Log(Vector2.Distance(gameObject.transform.position, daddest));

        if (dadenter&& Vector2.Distance(gameObject.transform.position, daddest) > 40)
        {
            dad.transform.position = daddest;

            dadenter = false;
        }

        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides w/ a door, teleport them to the spawn location of specified destination door
        if (other.gameObject.tag == "Door")
        {
            ChangeRoom thisDoor = other.gameObject.GetComponent<ChangeRoom>(); //get the "ChangeRoom" script of this door
            if (thisDoor.open==true)
            {
                gameObject.transform.position = thisDoor.dest; //teleport player to new destination
                if (sister_follow==true)
                {
                    sister.transform.position= thisDoor.dest;
                }
                if(dad_chase.dadhome==true)
                {
                    StartCoroutine(Wait(thisDoor));
                }
                camera.roomBounds = thisDoor.roomBounds;
                camera.transform.position = teleportCam(thisDoor); //teleport camera to new location, without causing lerp movement

                //sound fx for doors
                if (thisDoor.gameObject.name == "BottomDoor") //play key open sound when player use key to open the basement
                {
                    sound.playSound(SoundEffects.KeyOpen);
                }
                else
                {
                    sound.playSound(SoundEffects.DoorOpen); //play door open sound fx
                }            
            } else
            {
                if (thisDoor.gameObject.name == "BottomDoor") //play key locked sound when player doesn't have key and trying to open the basement
                {
                    sound.playSound(SoundEffects.KeyLocked);
                }
            }
        }
    }

    Vector3 teleportCam(ChangeRoom thisDoor)
    { //location to teleport the camera to the new room without any lerp movement
        BoxCollider2D roomBounds = thisDoor.roomBounds;
        float xMin = roomBounds.bounds.min.x; //left
        float xMax = roomBounds.bounds.max.x; //right
        float yMin = roomBounds.bounds.min.y; //top
        float yMax = roomBounds.bounds.max.y; //bottom
        float camSize = mainCam.orthographicSize; //half of camera height
        float camRatio = camSize * mainCam.aspect; //half of camera width
        float camY = Mathf.Clamp(thisDoor.dest.y, yMin + camSize, yMax - camSize); //keep y position inside room bounds
        float camX = Mathf.Clamp(thisDoor.dest.x, xMin + camRatio, xMax - camRatio); //keep x position inside room bounds

        return new Vector3(camX, camY, -10); //return location
    }
    IEnumerator Wait(ChangeRoom thisdoor)
    {
        yield return new WaitForSeconds(3);
        dadenter = true;
        daddest = thisdoor.dest;
    }

}

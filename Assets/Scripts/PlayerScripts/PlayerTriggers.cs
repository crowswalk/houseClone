using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
This script is responsible for detecting triggers that the player collides with.
*/
public class PlayerTriggers : MonoBehaviour
{
    public CamFollow thisCam;
    public static bool sister_follow;
    public GameObject sister;
    public GameObject dad;
    private bool dadenter;
    private Vector3 daddest;
    public Image spaceToUse;
    private Inventory inventory;
    public SoundManager sound;
    public GameObject doorLock;
    public Sprite lockIcon;
    public Sprite unlockIcon;
    void Start()
    {
        daddest = gameObject.transform.position;
        spaceToUse.enabled = false;
        inventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        //Debug.Log(Vector2.Distance(gameObject.transform.position, daddest));

        if (dadenter && Vector2.Distance(gameObject.transform.position, daddest) > 40)
        {
            dad.transform.position = daddest;
            dadenter = false;
        }
        if (inventory.holdingObj != null)
        {
            if (inventory.holdingObj.name.Contains("Key"))
            {
                doorLock.GetComponent<SpriteRenderer>().sprite = unlockIcon;
            }
            else
            {
                doorLock.GetComponent<SpriteRenderer>().sprite = lockIcon;
            }
        }
        else
        {
            doorLock.GetComponent<SpriteRenderer>().sprite = lockIcon;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collides w/ a door, teleport them to the spawn location of specified destination door
        if (other.gameObject.tag == "Door")
        {
            ChangeRoom thisDoor = other.gameObject.GetComponent<ChangeRoom>(); //get the "ChangeRoom" script of this door
            if (thisDoor.open == true)
            {
                gameObject.transform.position = thisDoor.dest; //teleport player to new destination
                if (sister_follow == true)
                {
                    sister.transform.position = thisDoor.dest;
                }
                if (dad_chase.dadhome == true)
                {
                    StartCoroutine(Wait(thisDoor));
                }
                thisCam.roomBounds = thisDoor.roomBounds;
                thisCam.transform.position = teleportCam(thisDoor); //teleport thisCam to new location, without causing lerp movement

                //sound fx for doors
                if (thisDoor.gameObject.name == "BottomDoor") //play key open sound when player use key to open the basement
                {
                    sound.playSound(SoundEffects.KeyOpen);
                }
                else
                {
                    sound.playSound(SoundEffects.DoorOpen); //play door open sound fx
                }
            }
            else
            {
                if (thisDoor.gameObject.name == "BottomDoor") //play key locked sound when player doesn't have key and trying to open the basement
                {
                    sound.playSound(SoundEffects.KeyLocked);
                }
            }
        }
        if (inventory.holdingObj != null)
        {
            if (other.gameObject.name == "bear_trap_checker" && inventory.holdingObj.name.Contains("BowlingBall"))
            {
                spaceToUse.enabled = true;
            }
            else if (other.gameObject.name == "PlungeTrigger" && inventory.holdingObj.name.Contains("Plunger"))
            {
                spaceToUse.enabled = true;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "bear_trap_checker" || other.gameObject.name == "PlungeTrigger")
        {
            spaceToUse.enabled = false;
        }
    }

    Vector3 teleportCam(ChangeRoom thisDoor)
    { //location to teleport the thisCam to the new room without any lerp movement
        BoxCollider2D roomBounds = thisDoor.roomBounds;
        float xMin = roomBounds.bounds.min.x; //left
        float xMax = roomBounds.bounds.max.x; //right
        float yMin = roomBounds.bounds.min.y; //top
        float yMax = roomBounds.bounds.max.y; //bottom
        float camSize = Camera.main.orthographicSize; //half of thisCam height
        float camRatio = camSize * Camera.main.aspect; //half of thisCam width
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

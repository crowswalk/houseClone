using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
This script keeps information about the door it is attached to. 
This information is used by PlayerTriggers to teleport the player and camera to new rooms.
*/
public class ChangeRoom : MonoBehaviour
{
    public BoxCollider2D roomBounds; //the destination's box collider (for the camera)
    public Transform destination; //where the player is transported to if they enter the door
    public Vector2 dest; //the position of where the player is transported to if they enter the door
    public bool needkey;
    public bool needaxe;
    public bool open;
    void Start()
    {
        dest = destination.position;
        if (needkey == false&&needaxe==false)
        {
           
            open = true;
        }
    }
    private void Update()
    {
        if (needkey == true)
        {
            if (Key.haskey)
            {
                open = true;
                
            }
            else {
                open = false;  
            }
        }
        if (needaxe == true)
        {
            if (blockeddoor.blockdoor==true)
            {
                open = true;
            }
            else
            {
                open = false;
            }
        }
    
    }

    //in final version, add some door animations here when the player interacts

}

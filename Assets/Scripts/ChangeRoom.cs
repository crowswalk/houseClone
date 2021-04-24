using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public BoxCollider2D roomBounds; //the destination's box collider (for the camera)
    public Transform destination; //where the player is transported to if they enter the door
    public Vector2 dest; //the position of where the player is transported to if they enter the door
    void Start() {
        dest = destination.position;
    }

    //in final version, add some door animations here when the player interacts

}

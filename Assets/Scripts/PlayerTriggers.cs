using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour {
    public CamFollow camera;
    public Camera mainCam;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Door") {
            ChangeRoom thisDoor = other.gameObject.GetComponent<ChangeRoom>();
            gameObject.transform.position = thisDoor.dest;
            camera.roomBounds = thisDoor.roomBounds;
            //Vector3 newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
            camera.transform.position = teleportCam(thisDoor);

        }
    }

    Vector3 teleportCam(ChangeRoom thisDoor) {
        BoxCollider2D roomBounds = thisDoor.roomBounds;
        float xMin = roomBounds.bounds.min.x; //left
        float xMax = roomBounds.bounds.max.x; //right
        float yMin = roomBounds.bounds.min.y; //top
        float yMax = roomBounds.bounds.max.y; //bottom
        float camSize = mainCam.orthographicSize; //half of camera height
        float camRatio = camSize * mainCam.aspect; //half of camera width
        float camY = Mathf.Clamp(thisDoor.dest.y, yMin + camSize, yMax - camSize); //keep y position inside world bounds
        float camX = Mathf.Clamp(thisDoor.dest.x, xMin + camRatio, xMax - camRatio); //keep x position inside world bounds

        return new Vector3(camX, camY, -10);
    }
}

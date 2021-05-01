using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{ //camera following behavior, doesn't cross room bounds
    public Transform playerTransform;
    public BoxCollider2D roomBounds; //box collider of room boundary
    public float smoothRate;

    private float xMin, xMax, yMin, yMax, camX, camY, camRatio, camSize; //minmax room coordinates, camera position and dimensions
    Camera mainCam;
    Vector3 smoothPos;
    Transform followTransform;


    void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        xMin = roomBounds.bounds.min.x; //left
        xMax = roomBounds.bounds.max.x; //right
        yMin = roomBounds.bounds.min.y; //top
        yMax = roomBounds.bounds.max.y; //bottom
        camSize = mainCam.orthographicSize; //half of camera height
        camRatio = camSize * mainCam.aspect; //half of camera width
        followTransform = playerTransform;
        camY = Mathf.Clamp(followTransform.position.y, yMin + camSize, yMax - camSize); //keep y position inside room bounds
        camX = Mathf.Clamp(followTransform.position.x, xMin + camRatio, xMax - camRatio); //keep x position inside room bounds
        smoothPos = Vector3.Lerp(gameObject.transform.position, new Vector3(camX, camY, gameObject.transform.position.z), smoothRate); //lerp position towards target
        gameObject.transform.position = smoothPos; //camera transform is at smoothed position


        if (bowling_ball.drop == true)
        {
            gameObject.transform.position = gameObject.transform.position + Random.insideUnitSphere * 2;
            StartCoroutine(ExampleCoroutine());
        }

        IEnumerator ExampleCoroutine()
        {
            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(0.5f);
            bowling_ball.drop = false;

        }
    }
}


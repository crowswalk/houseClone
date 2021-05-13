using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear_trap_drop : MonoBehaviour
{
    // Start is called before the first frame update
    private int inside;
    public GameObject beartrap;
    private bool drop;

    void Start()
    {
        inside = 0;
        drop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bowling_ball.drop == true && inside == 1)
        {
            drop = true;
        }
        if (drop)
        {
            beartrap.transform.position = beartrap.transform.position + new Vector3(0, -1, 0);
        }
        if (beartrap.transform.position.y < -244)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inside = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inside = 1;
        }
    }

}

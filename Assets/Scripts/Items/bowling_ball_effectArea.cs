using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowling_ball_effectArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<Cat>() != null)
        {
            collision.gameObject.GetComponent<Cat>().dead();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Cat>() != null)
        {
            collision.gameObject.GetComponent<Cat>().dead();
        }
    }
}

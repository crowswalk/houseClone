using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowling_ball : MonoBehaviour
{
    public static bool drop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CatBehavior>() != null)
        {
            CatBehavior cat = collision.gameObject.GetComponent<CatBehavior>();
        }
    }
}

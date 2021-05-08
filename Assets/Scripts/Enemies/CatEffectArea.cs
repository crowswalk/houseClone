using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEffectArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Cat>().kill(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<Cat>().kill(collision.gameObject);
        }
    }
}

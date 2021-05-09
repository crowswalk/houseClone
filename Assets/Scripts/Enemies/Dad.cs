using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            anim.SetBool("dead", true);
            GetComponent<dad_chase>().enabled = false;
            Destroy(collision.gameObject);
        }
    }
}

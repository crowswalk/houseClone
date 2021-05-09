using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_fridge : MonoBehaviour
{
    // Start is called before the first frame update
    int nearby;
    public static Animator animator;

    public SoundManager sound;
    void Start()
    {
        nearby = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearby==1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sound.playSound(SoundEffects.Fridge);
                animator.SetBool("open",true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearby = nearby + 1;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            nearby = nearby - 1;
        }
    }
}

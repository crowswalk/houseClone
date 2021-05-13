using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMonster : MonoBehaviour
{
    Animator animator;
    bool isDead;

    public SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<chasing_player>().speed > 0)
        {
            animator.SetBool("walk", true);
        } else
        {
            animator.SetBool("walk", false);
        }
    }

    public void dead()
    {
        sound.playSound(SoundEffects.RatDead);
        animator.SetBool("dead", true);
        this.gameObject.GetComponent<chasing_player>().enabled = false; //stop chasing player
        isDead = true;
        //StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) //kill player when collide with player
    {
        if (other.gameObject.tag == "Player" && !isDead)
        {
            sound.playSound(SoundEffects.RatKill);
            animator.SetBool("eat", true);
            gameObject.GetComponent<chasing_player>().enabled = false; //stop chasing player
            respwan.isdead = true;
            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }
    }
}

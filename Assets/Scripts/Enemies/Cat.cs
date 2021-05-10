using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public SoundManager sound;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dead()
    {
        sound.playSound(SoundEffects.CatDead);
        animator.SetBool("dead", true);
        StartCoroutine(ExampleCoroutine());
    }

    public void kill(GameObject g)
    {
        sound.playSound(SoundEffects.CatKill);
        animator.SetBool("kill", true);
        g.gameObject.SetActive(false);
        respwan.isdead = true;
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}

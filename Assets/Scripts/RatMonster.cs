using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMonster : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ratMonster_eat"))
        {
            animator.SetBool("eat", false);
        }
    }

    public void dead()
    {
        animator.SetBool("dead", true);
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("eat", true);
            Destroy(other.gameObject);
        }
    }
}

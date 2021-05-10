using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dad_chase : MonoBehaviour
{
    public int ray_distance;
    public static Animator animator;
    public static bool dadhome;
    public float speed;
    public float playerDist; //distance sister is from player
    public GameObject player;
    private SpriteRenderer mySpriteRenderer;
    public static float move_vertical;
    private int nearby;
    public static float move_horizontal;


    public LayerMask barrier;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerTriggers.sister_follow = true;
        animator = GetComponent<Animator>();
        dadhome = true;
        nearby = 0;
        move_vertical = 2;
        move_horizontal = 2;
    }

    // Update is called once per frame
    void Update()
    {
     

  
      
        Vector3 localPosition = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, localPosition, ray_distance, barrier);
        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * speed * move_horizontal, move_vertical * localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
       // Debug.Log(localPosition);
        if (Vector2.Distance(player.transform.position, transform.position) < playerDist)
        {
            move_vertical = 0;
            move_horizontal = 0;
        
            animator.SetBool("walk", false);
            animator.SetBool("prepare", true);
            StartCoroutine(preparekill());
          //  player.SetActive(false);
        }
        else
        {
            animator.SetBool("walk", true);
            if (nearby==1)
            {
               // Debug.Log("I am hit");
             
                //move_vertical = localPosition.y/localPosition.x;
                //move_horizontal = -1*localPosition.x/localPosition.y;

               // StartCoroutine(ExampleCoroutine());
            }
         
        }
        if (player.transform.position.x > transform.position.x)
        {
            mySpriteRenderer.flipX = false;
        }
        else
        {
            mySpriteRenderer.flipX = true;
        }

       
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);
        move_vertical = 2;
        move_horizontal = 2;
    }

    IEnumerator preparekill()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(1.7f);
        if (Vector2.Distance(player.transform.position, transform.position) <playerDist)
        {
            move_vertical = 0;
            move_horizontal = 0;

            animator.SetBool("walk", false);
            animator.SetBool("dad_kill", true);
            respwan.isdead = true;
            player.SetActive(false);
        }
        else {
            move_vertical = 2;
            move_horizontal = 2;
            animator.SetTrigger("escape1");
            animator.SetBool("prepare", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {


        nearby = nearby + 1;

    }
    private void OnCollisionExit2D(Collision2D other)
    {

        nearby = nearby - 1;

    }
}


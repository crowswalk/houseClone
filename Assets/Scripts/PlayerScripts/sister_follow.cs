using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sister_follow : MonoBehaviour
{
    public int ray_distance;
    public static Animator animator;
    public float speed;
    public float playerDist; //distance sister is from player
    public GameObject player;
    private SpriteRenderer mySpriteRenderer;
    public static int move_vertical;
    public static int move_horizontal;
  

    public LayerMask barrier;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        PlayerTriggers.sister_follow = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, ray_distance, barrier);
        Vector3 localPosition = player.transform.position - transform.position;

        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * speed * move_vertical, move_horizontal * localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);

        if (Vector2.Distance(player.transform.position, transform.position) < playerDist)
        {
            move_vertical = 0;
            move_horizontal = 0;

            animator.SetBool("still", true);
        }
        else
        {
            animator.SetBool("still", false);
            if (hit.collider != null)
            {

                move_vertical = 0;
                move_horizontal = 4;


            }
            else
            {
                move_vertical = 2;
                move_horizontal = 2;
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

}

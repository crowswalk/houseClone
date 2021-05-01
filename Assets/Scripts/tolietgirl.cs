using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tolietgirl : MonoBehaviour
{
    public GameObject player;
    Animator animator;
    public MovePlayer player_move;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 30 && player_move.dir.x <0 &&plunger.use==true)
        {
            plunger.use = true;
            animator.SetBool("dead", true);
            StartCoroutine(ExampleCoroutine());
            Destroy(GetComponent<BoxCollider2D>());
        }
       
;    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);
        
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("eat", true);
            Destroy(player);
        }
    }
}

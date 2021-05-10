using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tolietgirl : MonoBehaviour
{
    public GameObject player;
    Animator animator;
    public MovePlayer player_move;
    public int girlDist;

    public SoundManager sound;
    private bool deadSoundPlayed = false; //checking the dead sound has played already

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < girlDist)
        {
            animator.enabled = true;
        }
        if (Vector2.Distance(player.transform.position, transform.position) < 30 && player_move.dir.x < 0 && plunger.use)
        {
            if (!deadSoundPlayed)
            {
                sound.playSound(SoundEffects.TolietgirlDead);
                deadSoundPlayed = true;
            }
            animator.SetBool("dead", true);
            //StartCoroutine(ExampleCoroutine());
            Destroy(GetComponent<BoxCollider2D>());
        }

        if (Input.GetKeyDown(KeyCode.R)) //if restart, change the dead sound played to false
        {
            deadSoundPlayed = false;
        }
;
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sound.playSound(SoundEffects.TolietgirlKill);
            animator.SetBool("eat", true);
            player.SetActive(false);
            respwan.isdead= true;
        }
    }
}

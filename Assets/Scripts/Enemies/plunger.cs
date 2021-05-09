using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plunger : MonoBehaviour
{
    public static bool use;
    public MovePlayer player;

    void Update()
    {
        if (use && player.canMove)
        {
            player.showPlungeSprite();
            StartCoroutine(ExampleCoroutine());
        }
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(.5f);
        use = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowling_ball : MonoBehaviour
{
    public static bool drop;
    public GameObject bowlingEffectArea;
    public Inventory playerInv;

    public SoundManager sound;

    public void dropBowling()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            sound.playSound(SoundEffects.BowlingDrop);
            Instantiate(bowlingEffectArea, transform.position, transform.rotation);
        }
    }
}

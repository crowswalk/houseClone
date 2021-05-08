using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowling_ball : MonoBehaviour
{
    public static bool drop;
    public GameObject bowlingEffectArea;
    public Inventory playerInv;

    public void dropBowling()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(bowlingEffectArea, transform.position, transform.rotation);
        }
    }
}

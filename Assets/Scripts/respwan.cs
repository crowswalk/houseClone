using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respwan : MonoBehaviour
{
    public GameObject player;
    public GameObject text;
    public static bool isdead;

    void Start()
    {
        isdead = false;
    }

    void Update()
    {
   

            if(isdead)
        {
            StartCoroutine(showtext());
        }
        
    }
    IEnumerator showtext()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(3f);
        text.SetActive(true);
    }
}

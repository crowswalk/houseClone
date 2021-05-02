using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool useaxe;
    void Start()
    {
        useaxe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (useaxe == true)
        {
            StartCoroutine(ExampleCoroutine());
        }
      

    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(1.0f);

        useaxe = false;
    }
}

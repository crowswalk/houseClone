using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plunger : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool use;
    void Start()
    {
        use = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (use==true)
        {
            StartCoroutine(ExampleCoroutine());
        }
   
    }
    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(1.0f);

        use = false;
    }
}

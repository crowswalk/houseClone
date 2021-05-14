using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dad_show_up : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.currenttimeHr >= 3)
        {
            dad.SetActive(true);
            Destroy(gameObject);
        }
    }
}

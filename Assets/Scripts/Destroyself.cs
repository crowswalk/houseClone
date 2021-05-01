using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyself : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
    [Range(0.0f,10.0f)]
=======
    [Range(0.0f, 10.0f)]
>>>>>>> testShooting
    float timeEnd;

    float timeStart;
    float timeCurrent;
    // Start is called before the first frame update
    void Start()
    {
        timeStart = 0;
        timeCurrent = timeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCurrent < timeEnd)
        {
            timeCurrent += Time.deltaTime;
<<<<<<< HEAD
        } else
=======
        }
        else
>>>>>>> testShooting
        {
            timeCurrent = timeStart;
            Destroy(gameObject);
        }
    }
}
<<<<<<< HEAD
=======

>>>>>>> testShooting

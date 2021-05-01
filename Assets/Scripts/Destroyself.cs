using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyself : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
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
        }
        else
        {
            timeCurrent = timeStart;
            Destroy(gameObject);
        }
    }
}


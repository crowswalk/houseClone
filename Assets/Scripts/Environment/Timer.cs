using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //time loop
    [SerializeField]
    [Range(0,2.0f)]
    float loopTimeReset; //longer rest time means longer duration for 1 minute
    float loopTime; //every time loopTime <= 0, clock goes 1 minute
    public bool loopTimeCountDown = false;

    public int clockCurrentTimeHr, clockCurrentTimeMin, clockStartTimeHr, clockStartTimeMin, clockEndTimeHr, clockEndTimeMin;
    public static int currenttimeHr;
    //time loop clock showing on screen
    public GameObject clockText;
    public GameObject clockTextBG;

    public Text clockTextComponent;

    // Start is called before the first frame update
    void Start()
    {
        //time loop
        loopTime = loopTimeReset;
        clockCurrentTimeHr = clockStartTimeHr;
        clockCurrentTimeMin = clockStartTimeMin;

        loopTimeCountDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        currenttimeHr = clockCurrentTimeHr;
        //time loop
        showClockText(); //set clock active on screen
        clockCountDown(); //make clock counting down

        if (clockCurrentTimeHr == clockEndTimeHr && clockCurrentTimeMin == clockEndTimeMin) //reset the clock when current time == end time
        {
            resetClock();
        }
    }



    void clockCountDown()
    {
        if (loopTimeCountDown)
        {
            loopTime -= Time.deltaTime;
            if (loopTime <= 0) //if loopTime ends, 1 minute is spent, which means clock time + 1 min
            {
                if (clockCurrentTimeMin <= 59)
                {
                    clockCurrentTimeMin++;
                }
                else
                {
                    clockCurrentTimeMin = 0;
                    clockCurrentTimeHr++;
                }

                loopTime = loopTimeReset;
            }
        }
    }

    public void showClockText() //show clock on screen as "hr : min a.m./p.m."
    {
        clockText.SetActive(true);
        clockTextBG.SetActive(true);
        if (clockCurrentTimeHr < 10)
        {
            if (clockCurrentTimeMin < 10)
            {
                clockTextComponent.text = "0" + clockCurrentTimeHr.ToString() + ":" + "0" + clockCurrentTimeMin.ToString() + "a.m.";
            }
            else
            {
                clockTextComponent.text = "0" + clockCurrentTimeHr.ToString() + ":" + clockCurrentTimeMin.ToString() + "a.m.";
            }
        } else if (clockCurrentTimeHr<=12)
        {
            if (clockCurrentTimeMin < 10)
            {
                clockTextComponent.text = clockCurrentTimeHr.ToString() + ":" + "0" + clockCurrentTimeMin.ToString() + "a.m.";
            } else
            {
                clockTextComponent.text = clockCurrentTimeHr.ToString() + ":" + clockCurrentTimeMin.ToString() + "a.m.";
            }
        } else
        {
            int currentHr = clockCurrentTimeHr - 12;
            if (currentHr < 10)
            {
                if (clockCurrentTimeMin < 10)
                {
                    clockTextComponent.text = "0" + currentHr.ToString() + ":" + "0" + clockCurrentTimeMin.ToString() + "p.m.";
                }
                else
                {
                    clockTextComponent.text = "0" + currentHr.ToString() + ":" + clockCurrentTimeMin.ToString() + "p.m.";
                }
            } else
            {
                if (clockCurrentTimeMin < 10)
                {
                    clockTextComponent.text = currentHr.ToString() + ":" + "0" + clockCurrentTimeMin.ToString() + "p.m.";
                }
                else
                {
                    clockTextComponent.text = currentHr.ToString() + ":" + clockCurrentTimeMin.ToString() + "p.m.";
                }
            }
        }
    }

    public void resetClock() //reset the clock to start time
    {
        clockCurrentTimeHr = clockStartTimeHr;
        clockCurrentTimeMin = clockStartTimeMin;
        loopTime = loopTimeReset;
    }
}

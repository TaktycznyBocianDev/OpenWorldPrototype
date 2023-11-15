using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DayNightCycle : MonoBehaviour
{
    [Header("Which part of day we have now? - MORNING,NOON,EVENING,MIDNIGHT")]
    public DayTimes currentDayPart;
    [Space(5)]
    [Header("How many days pass?")]
    public int daysCounter = 1;
    [Space(5)]
    [Header("What is current hour on a clock?")]
    [SerializeField] int dayCurrentTime;
    [Header("How much hours make a day? (ONLY DAY, NO DAY+NIGHT!)")]
    [SerializeField] int fullDayTime;
    [Header("Set to 1 means that 1h in game is 1s in real world")]
    [SerializeField] float hourScale;


    private void Start()
    {
        //When we start, we want to set everything and start our clock
        SetTime(0, DayTimes.MORNING);
        StartCoroutine(Time());
    }

    public void SetTime(int wantedTime, DayTimes wantedDayTime)
    {
        dayCurrentTime = wantedTime;
        currentDayPart = wantedDayTime;
    }

    IEnumerator Time()
    {
        while (true)
        {
            timeCount();
            DayTimes actualDayTime = currentDayPart;

            //Change current part of the day, if needed
            if (dayCurrentTime >= 0 && dayCurrentTime <= fullDayTime/2)
            {
                currentDayPart = DayTimes.MORNING;
            }
            if (dayCurrentTime > fullDayTime / 2 && dayCurrentTime <= fullDayTime)
            {
                currentDayPart = DayTimes.NOON;
            }
            if (dayCurrentTime > fullDayTime && dayCurrentTime <= fullDayTime*1.5)
            {
                currentDayPart = DayTimes.EVENING;
            }
            if (dayCurrentTime > fullDayTime * 1.5 && dayCurrentTime <= fullDayTime*2)
            {
                currentDayPart = DayTimes.MIDNIGHT;
            }

            if (dayCurrentTime >= fullDayTime*2)
            {
                dayCurrentTime = 0;
                daysCounter++;
            }
            Presentate();
            yield return new WaitForSeconds(hourScale);
        }
    }

    void timeCount()
    {
        dayCurrentTime += 1;
    }

     void Presentate()
    {
        Debug.Log("-------------------------------------");
        Debug.Log("Current time is: " + UnityEngine.Time.time);
        Debug.Log("My time is: " + dayCurrentTime);
        Debug.Log("Current day time is: " + currentDayPart);
        Debug.Log("Day: " + daysCounter);
        Debug.Log("-------------------------------------");
    }
}

public enum DayTimes
{
    MORNING,
    NOON,
    EVENING,
    MIDNIGHT
}

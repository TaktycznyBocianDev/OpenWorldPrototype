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
    public int daysCounter;
    [Space(5)]
    [Header("What is current hour on a clock?")]
    [SerializeField] int dayCurrentTime;
    [Header("How much hours make a day? (DAY+NIGHT)")]
    [SerializeField] int fullDayTime;
    public int GetDayTime(SunSpin sunSpinComponent) { return fullDayTime; } //only sun could have this         
    [Header("Set to 1 means that 1h in game is 1s in real world")]
    [SerializeField] float hourScale;
    public float GetHourScale(SunSpin sunSpinComponent) { return hourScale; } //only sun could have this 

    private void Start()
    {
        //When we start, we want to set everything and start our clock
        SetTime(0, DayTimes.MORNING);
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while (true)
        {
            dayCurrentTime += 1;
            DayTimes actualDayTime = currentDayPart;

            //Change current part of the day, if needed
            if (dayCurrentTime >= 0 && dayCurrentTime <= fullDayTime/4)
            {
                currentDayPart = DayTimes.MORNING; //1 quarter of day...
            }
            if (dayCurrentTime > fullDayTime / 4 && dayCurrentTime <= fullDayTime/2)
            {
                currentDayPart = DayTimes.NOON; // ... 2 quarter...
            }
            if (dayCurrentTime > fullDayTime/2 && dayCurrentTime <= fullDayTime*0.75f)
            {
                currentDayPart = DayTimes.EVENING; // ... 3 quarter...
            }
            if (dayCurrentTime > fullDayTime * 0.75f && dayCurrentTime <= fullDayTime)
            {
                currentDayPart = DayTimes.MIDNIGHT; // ... 4 quarter...
            }

            if (actualDayTime != currentDayPart)
            {
                //Tell about this!
            }

            if (dayCurrentTime >= fullDayTime)
            {
                dayCurrentTime = 0;
                daysCounter++;
            }

            //Presentate();

            yield return new WaitForSeconds(hourScale);
        }
    }


    public void SetTime(int wantedTime, DayTimes wantedDayTime)
    {
        dayCurrentTime = wantedTime;
        currentDayPart = wantedDayTime;
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

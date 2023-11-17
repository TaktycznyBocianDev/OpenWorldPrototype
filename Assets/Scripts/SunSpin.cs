using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DayNightCycle))]
public class SunSpin : MonoBehaviour
{

    [SerializeField] DayNightCycle dayNightCycle;
    int fullDayTime;
    float hourScale;

    void Start()
    {
        fullDayTime = dayNightCycle.GetDayTime(this);
        hourScale = dayNightCycle.GetHourScale(this);
    }
    private void Update()
    {
        // 460 degrees of full spin / how many "hours" game day have * how much seconds make and "hour" * Time.makeItEven
        transform.Rotate((360f / fullDayTime * hourScale * Time.deltaTime), 0, 0, Space.Self);
    }
}

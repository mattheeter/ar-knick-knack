using System;
using UnityEngine;

public class GetTime : MonoBehaviour
{
    public GameObject timeTextObject;
    public string timezone;

    private TimeZoneInfo targetZone;

    void Start()
    {
        targetZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
    }

    void Update()
    {
        // Fetch the time.
        DateTime nowTime = DateTime.Now;
        
        // Convert to the provided time zone.
        DateTime time = TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local, targetZone);
        
        // Display it.
        timeTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = time + "\nlocal time";
    }
}

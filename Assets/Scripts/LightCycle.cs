using System;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    // Changing the angle of the sunlight based on the time of day.
    public Transform lightTransform;
    public string timezone;
    
    private TimeZoneInfo targetZone;
    
    void Start()
    {
        targetZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch the time.
        DateTime nowTime = DateTime.Now;
        
        // Convert to the provided time zone.
        DateTime time = TimeZoneInfo.ConvertTime(nowTime, TimeZoneInfo.Local, targetZone);
        
        // Convert from 0-24 hours to 0-360 degrees.
        // 270 is midnight, 90 is noon
        float angle = ((time.Hour / 24f) * 360) + 270;
        
        // Rotate about the x-axis.
        lightTransform.localRotation = Quaternion.Euler(angle, 0, 0);
    }
}

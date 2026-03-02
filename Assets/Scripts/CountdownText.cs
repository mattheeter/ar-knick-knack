using System;
using TMPro;
using UnityEngine;

public class CountdownText : MonoBehaviour
{
    public GameObject countdownTextObject;
    public string eventDate;

    private DateTime eventDateTime; 
    
    void Start()
    {
        // Convert the string (mm-dd-yyyy) format to a DateTime    
        int month = int.Parse(eventDate.Split("-")[0]);
        int day = int.Parse(eventDate.Split("-")[1]);
        int year = int.Parse(eventDate.Split("-")[2]);
        eventDateTime = new DateTime(year, month, day);
    }

    void Update()
    {
        // Get the difference between now and the event date.
        TimeSpan timeDelta = eventDateTime - DateTime.Now;
        if (timeDelta.Days == 0)
        {
            countdownTextObject.GetComponent<TextMeshProUGUI>().text = "Today's the day!";
            return;
        }
        if (timeDelta.Days < 0)
        {
            countdownTextObject.GetComponent<TextMeshProUGUI>().text = "The event has passed!";
            return;
        }
        countdownTextObject.GetComponent<TextMeshProUGUI>().text = timeDelta.Days + " days left!";
    }
}

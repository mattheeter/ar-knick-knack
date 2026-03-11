using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class WeatherAPI : MonoBehaviour
{
     public GameObject weatherTextObject;
     public GameObject rain;
    
    public string url;
    private GameObject rainObject;

    void Start()
    {
        InvokeRepeating("GetDataFromWeb", 2f, 10f);
    }

    void GetDataFromWeb()
   {
       StartCoroutine(GetRequest(url));
   }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result ==  UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
            	// grab the current temperature and simplify it if needed
            	int startTemp = webRequest.downloadHandler.text.IndexOf("temp",0);
            	int endTemp = webRequest.downloadHandler.text.IndexOf(",",startTemp);
				double tempF = float.Parse(webRequest.downloadHandler.text.Substring(startTemp+6, (endTemp-startTemp-6)));
				int easyTempF = Mathf.RoundToInt((float)tempF);
                int startConditions = webRequest.downloadHandler.text.IndexOf("main",0);
                int endConditions = webRequest.downloadHandler.text.IndexOf(",",startConditions);
                string conditions = webRequest.downloadHandler.text.Substring(startConditions+7, (endConditions-startConditions-8));

                weatherTextObject.GetComponent<TextMeshProUGUI>().text = easyTempF + "°F\n" + conditions;
                
                Destroy(rainObject);
                
                if (
                    conditions == "Rain" || 
                    conditions == "Thunderstorm" || 
                    conditions == "Drizzle" ||
                    conditions == "Tornado"
                    )
                {
                    // Add the rain to the scene based on if the above conditions are met
                    // https://openweathermap.org/weather-conditions
                    rainObject = Instantiate(rain);
                }
            }
        }
    }
}

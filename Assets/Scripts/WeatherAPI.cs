using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class WeatherAPI : MonoBehaviour
{
     public GameObject weatherTextObject;
    
    string url = "https://api.openweathermap.org/data/2.5/weather?lat=26.2306&lon=-80.1251&APPID=c8a4bfd031b20b5c794089f1ade0288b&units=imperial";

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
            }
        }
    }
}
